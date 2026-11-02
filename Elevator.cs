namespace ElevatorApp;

/// <summary>Observer interface: the GUI (and anything else) subscribes to an elevator.</summary>
public interface IElevatorObserver
{
    void OnPositionChanged(Elevator e);
    void OnDoorsChanged(Elevator e);
    void OnStateChanged(Elevator e);
    void OnArrived(Elevator e, int floor);
}

/// <summary>State design pattern: every state decides how the elevator reacts to a timer tick or a request.</summary>
public interface IElevatorState
{
    string Name { get; }
    void Enter(Elevator e);
    void Tick(Elevator e);
    void AddRequest(Elevator e, int floor);
}

public class Elevator
{
    public const int TopFloor = 1;                 // floors 0 and 1
    public const double TickSeconds = 0.02;        // timer interval (20 ms)
    public const double MoveStep = 0.01;           // fraction of a floor per tick -> 2 s per floor
    public const double SecondsPerFloor = TickSeconds / MoveStep;
    public const double DoorCycleSeconds = DoorsOpenState.TotalTicks * TickSeconds;

    private readonly List<IElevatorObserver> _observers = new();

    public int Id { get; }
    public string Name { get; }

    /// <summary>0.0 = floor 0, 1.0 = floor 1; values in between while moving.</summary>
    public double Position { get; set; }

    /// <summary>0.0 = doors (car door and floor door) closed, 1.0 = fully open.</summary>
    public double DoorOpenness { get; set; }

    public List<int> Targets { get; } = new();
    public IElevatorState State { get; private set; } = null!;

    public int CurrentFloor => (int)Math.Round(Position);
    public bool AtFloor => Math.Abs(Position - CurrentFloor) < 0.0001;

    public Elevator(int id, string name)
    {
        Id = id;
        Name = name;
        SetState(new IdleState());
    }

    public void Subscribe(IElevatorObserver observer) => _observers.Add(observer);

    public void SetState(IElevatorState state)
    {
        State = state;
        state.Enter(this);
        foreach (var o in _observers) o.OnStateChanged(this);
    }

    public void Tick() => State.Tick(this);

    public bool AddRequest(int floor)
    {
        if (floor < 0 || floor > TopFloor) return false;   // robustness: ignore invalid floors
        State.AddRequest(this, floor);
        return true;
    }

    /// <summary>Door open button: only works while the car is stopped at a floor.</summary>
    public bool OpenDoor()
    {
        if (!AtFloor) return false;
        if (State is IdleState)
        {
            Targets.Remove(CurrentFloor);
            SetState(new DoorsOpenState());
            return true;
        }
        if (State is DoorsOpenState doors)
        {
            doors.Reopen(this);
            return true;
        }
        return false;
    }

    /// <summary>Door close button: starts closing right away.</summary>
    public bool CloseDoor()
    {
        if (State is not DoorsOpenState doors) return false;
        doors.CloseNow(this);
        return true;
    }

    public void TriggerSos()
    {
        if (State is SosState) return;
        SetState(new SosState());
    }

    public void ResetSos()
    {
        if (State is not SosState) return;
        if (AtFloor) SetState(new DoorsOpenState());
        else
        {
            Targets.Clear();
            Targets.Add(CurrentFloor);     // nearest floor
            SetState(new IdleState());
        }
    }

    public void NotifyPosition() { foreach (var o in _observers) o.OnPositionChanged(this); }
    public void NotifyDoors() { foreach (var o in _observers) o.OnDoorsChanged(this); }
    public void NotifyArrived(int floor) { foreach (var o in _observers) o.OnArrived(this, floor); }

    /// <summary>Estimated seconds until this elevator could reach the given floor.</summary>
    public double EstimateResponse(int floor)
    {
        if (State is SosState) return double.PositiveInfinity;   // out of service

        double time = 0;
        double pos = Position;

        if (State is DoorsOpenState doors) time += doors.RemainingSeconds;

        foreach (int target in Targets)
        {
            time += Math.Abs(target - pos) * SecondsPerFloor;
            if (target == floor) return time;
            time += DoorCycleSeconds;
            pos = target;
        }

        return time + Math.Abs(floor - pos) * SecondsPerFloor;
    }
}

public class IdleState : IElevatorState
{
    public string Name => "Idle";

    public void Enter(Elevator e) { }

    public void Tick(Elevator e)
    {
        if (e.Targets.Count == 0) return;

        if (e.AtFloor && e.Targets.Contains(e.CurrentFloor))
        {
            e.Targets.Remove(e.CurrentFloor);
            e.SetState(new DoorsOpenState());
            return;
        }

        int target = e.Targets[0];
        e.SetState(target > e.Position ? new MovingUpState() : new MovingDownState());
    }

    public void AddRequest(Elevator e, int floor)
    {
        if (!e.Targets.Contains(floor)) e.Targets.Add(floor);
    }
}

public abstract class MovingState : IElevatorState
{
    protected abstract double Direction { get; }
    public abstract string Name { get; }

    public void Enter(Elevator e) { e.DoorOpenness = 0; }   // doors stay closed between floors

    public void Tick(Elevator e)
    {
        e.Position = Math.Clamp(e.Position + Direction * Elevator.MoveStep, 0, Elevator.TopFloor);
        e.NotifyPosition();

        // nearest requested floor in the direction of travel
        var ahead = e.Targets
            .Where(t => (t - e.Position) * Direction > -0.0001)
            .OrderBy(t => Math.Abs(t - e.Position))
            .ToList();

        if (ahead.Count == 0)
        {
            e.SetState(new IdleState());
            return;
        }

        int target = ahead[0];
        if ((target - e.Position) * Direction <= 0.0001)
        {
            e.Position = target;
            e.NotifyPosition();
            e.Targets.Remove(target);
            e.SetState(new DoorsOpenState());
        }
    }

    public void AddRequest(Elevator e, int floor)
    {
        if (!e.Targets.Contains(floor)) e.Targets.Add(floor);
    }
}

public class MovingUpState : MovingState
{
    protected override double Direction => 1;
    public override string Name => "Moving up";
}

public class MovingDownState : MovingState
{
    protected override double Direction => -1;
    public override string Name => "Moving down";
}

/// <summary>Opens the doors, waits a while, then closes them (car door and floor door move together).</summary>
public class DoorsOpenState : IElevatorState
{
    public const int OpenTicks = 25;
    public const int HoldTicks = 100;
    public const int CloseTicks = 25;
    public const int TotalTicks = OpenTicks + HoldTicks + CloseTicks;

    private int _ticks;

    public string Name => "Doors open";

    public double RemainingSeconds => Math.Max(0, TotalTicks - _ticks) * Elevator.TickSeconds;

    public double HoldRemainingSeconds =>
        _ticks <= OpenTicks ? HoldTicks * Elevator.TickSeconds
        : _ticks <= OpenTicks + HoldTicks ? (OpenTicks + HoldTicks - _ticks) * Elevator.TickSeconds
        : 0;

    public void Enter(Elevator e)
    {
        _ticks = (int)Math.Round(e.DoorOpenness * OpenTicks);   // continue from current opening
        e.NotifyArrived(e.CurrentFloor);
    }

    /// <summary>Keep or reopen the doors (same floor pressed again / open-door button).</summary>
    public void Reopen(Elevator e)
    {
        _ticks = _ticks > OpenTicks + HoldTicks
            ? (int)Math.Round(e.DoorOpenness * OpenTicks)
            : Math.Min(_ticks, OpenTicks);
        e.NotifyArrived(e.CurrentFloor);
    }

    /// <summary>Close-door button.</summary>
    public void CloseNow(Elevator e)
    {
        if (_ticks <= OpenTicks + HoldTicks)
            _ticks = OpenTicks + HoldTicks + (int)Math.Round((1 - e.DoorOpenness) * CloseTicks);
    }

    public void Tick(Elevator e)
    {
        _ticks++;

        if (_ticks <= OpenTicks) e.DoorOpenness = (double)_ticks / OpenTicks;
        else if (_ticks <= OpenTicks + HoldTicks) e.DoorOpenness = 1;
        else if (_ticks <= TotalTicks) e.DoorOpenness = 1.0 - (double)(_ticks - OpenTicks - HoldTicks) / CloseTicks;
        else
        {
            e.DoorOpenness = 0;
            e.NotifyDoors();
            e.SetState(new IdleState());
            return;
        }

        e.NotifyDoors();
    }

    public void AddRequest(Elevator e, int floor)
    {
        if (floor == e.CurrentFloor)
        {
            Reopen(e);      // same floor pressed again: keep / reopen the doors
            return;
        }

        if (!e.Targets.Contains(floor)) e.Targets.Add(floor);

        // a floor button was pressed inside the car: close the doors now
        if (_ticks > OpenTicks && _ticks <= OpenTicks + HoldTicks)
            _ticks = OpenTicks + HoldTicks;
    }
}

/// <summary>Emergency: the car stops where it is, requests are cleared, doors open if it is at a floor.</summary>
public class SosState : IElevatorState
{
    public string Name => "SOS - stopped";

    public void Enter(Elevator e) { e.Targets.Clear(); }

    public void Tick(Elevator e)
    {
        if (e.AtFloor && e.DoorOpenness < 1)
        {
            e.DoorOpenness = Math.Min(1, e.DoorOpenness + 0.04);
            e.NotifyDoors();
        }
    }

    public void AddRequest(Elevator e, int floor) { }   // ignored until the SOS is reset
}
