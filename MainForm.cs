using System.Data;

namespace ElevatorApp;

/// <summary>
/// Game logic and event handlers. The visual layout lives in MainForm.Designer.cs,
/// so it can be edited in Visual Studio's Design view.
/// </summary>
public partial class MainForm : Form, IElevatorObserver
{
    private static readonly Color LightOn = Color.Gold;
    private static readonly Color LightOff = Color.Gainsboro;

    private const int ElevatorCount = 2;
    private const int FloorCount = Elevator.TopFloor + 1;

    private readonly List<Elevator> _elevators = new();
    private readonly Dispatcher _dispatcher;
    private readonly EventLogger _logger = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = (int)(Elevator.TickSeconds * 1000) };

    // the controls from the designer, grouped by elevator index (0 = A, 1 = B) or floor
    private readonly ShaftView[] _shafts;
    private readonly InteriorView[] _interiors;
    private readonly Label[] _displays;
    private readonly Label[] _doorTimers;
    private readonly Label[] _stateLabels;
    private readonly Button[] _sosButtons;
    private readonly Button[] _openButtons;
    private readonly Button[] _closeButtons;
    private readonly Button[,] _carButtons;
    private readonly Button[] _hallButtons;

    private int _tickCount;
    private bool _blink;
    private LogForm? _logForm;

    public MainForm()
    {
        Settings.Load();
        InitializeComponent();

        _shafts = new[] { shaftA, shaftB };
        _interiors = new[] { interiorA, interiorB };
        _displays = new[] { lblDisplayA, lblDisplayB };
        _doorTimers = new[] { lblDoorTimerA, lblDoorTimerB };
        _stateLabels = new[] { lblStateA, lblStateB };
        _sosButtons = new[] { btnSosA, btnSosB };
        _openButtons = new[] { btnOpenA, btnOpenB };
        _closeButtons = new[] { btnCloseA, btnCloseB };
        _carButtons = new[,] { { btnFloor0A, btnFloor1A }, { btnFloor0B, btnFloor1B } };
        _hallButtons = new[] { btnHall0, btnHall1 };

        string[] names = { "Elevator A", "Elevator B" };
        for (int i = 0; i < ElevatorCount; i++)
        {
            var elevator = new Elevator(i + 1, names[i]);
            elevator.Subscribe(this);
            _elevators.Add(elevator);
        }
        _dispatcher = new Dispatcher(_elevators);

        // button events
        for (int f = 0; f < FloorCount; f++)
        {
            int floor = f;
            _hallButtons[f].Click += (_, _) => OnHallRequest(floor);
        }
        for (int i = 0; i < ElevatorCount; i++)
        {
            int index = i;
            _sosButtons[i].Click += (_, _) => OnSos(index);
            _openButtons[i].Click += (_, _) => OnOpenDoor(index);
            _closeButtons[i].Click += (_, _) => OnCloseDoor(index);
            for (int f = 0; f < FloorCount; f++)
            {
                int floor = f;
                _carButtons[i, f].Click += (_, _) => OnCarRequest(index, floor);
            }
        }
        btnReconnect.Click += (_, _) => ConnectDatabase();
        btnOpenLog.Click += (_, _) => OpenLogWindow();

        _logger.Failed += message => SetStatus("Database logging stopped: " + message, true);
        _logger.Deleted += count => SetStatus($"Deleted {count} log row(s).", false);
        _logger.DeleteFailed += message => SetStatus("Delete failed: " + message, true);

        _timer.Tick += OnTimerTick;
        Load += (_, _) =>
        {
            foreach (var elevator in _elevators) RefreshView(elevator);
            lblClock.Text = DateTime.Now.ToString("ddd dd MMM yyyy   HH:mm:ss");
            _timer.Start();
            ConnectDatabase();
        };
        FormClosing += (_, _) => _timer.Stop();
    }

    // ---------- timer: animation, clock, alarm blinking ----------

    private void OnTimerTick(object? sender, EventArgs e)
    {
        foreach (var elevator in _elevators) elevator.Tick();     // moves cars and doors (Task 4)

        _tickCount++;
        if (_tickCount % 10 == 0)
            lblClock.Text = DateTime.Now.ToString("ddd dd MMM yyyy   HH:mm:ss");

        if (_tickCount % 25 == 0)
        {
            _blink = !_blink;
            for (int i = 0; i < ElevatorCount; i++)
            {
                _interiors[i].Blink = _blink;
                if (_interiors[i].Alarm) _interiors[i].Invalidate();
            }
        }
    }

    // ---------- database connection (BackgroundWorker) ----------

    private void ConnectDatabase()
    {
        btnReconnect.Enabled = false;
        SetStatus("Connecting to SQL Server...", false);

        var worker = new System.ComponentModel.BackgroundWorker();
        worker.DoWork += (_, e) =>
        {
            try { Db.EnsureCreated(); }
            catch (Exception ex) { e.Result = ex; }
        };
        worker.RunWorkerCompleted += (_, e) =>
        {
            btnReconnect.Enabled = true;
            if (e.Result is Exception ex)
            {
                _logger.Enabled = false;
                SetStatus("SQL Server not available - running without database. Check appsettings.json. (" + ex.Message + ")", true);
                return;
            }
            _logger.Enabled = true;
            SetStatus("Connected to SQL Server - database ElevatorDB.", false);
            _logger.Log(null, "Application", null, "System started");
        };
        worker.RunWorkerAsync();
    }

    private void SetStatus(string text, bool error)
    {
        lblStatus.Text = text;
        lblStatus.ForeColor = error ? Color.Firebrick : Color.DimGray;
    }

    // ---------- event log window ----------

    private void OpenLogWindow()
    {
        if (_logForm != null && !_logForm.IsDisposed)
        {
            if (_logForm.WindowState == FormWindowState.Minimized) _logForm.WindowState = FormWindowState.Normal;
            _logForm.Activate();
            return;
        }

        _logForm = new LogForm(_logger);
        _logForm.Show(this);
    }

    // ---------- user actions ----------

    private void OnHallRequest(int floor)
    {
        var elevator = _dispatcher.HandleFloorRequest(floor);
        if (elevator == null)
        {
            SetStatus("No elevator is available (all are in SOS mode).", true);
            return;
        }
        _hallButtons[floor].BackColor = LightOn;                  // button light on
        _logger.Log(elevator.Id, "Floor request", floor, $"Request button on floor {floor} assigned to {elevator.Name}");
    }

    private void OnCarRequest(int index, int floor)
    {
        var elevator = _elevators[index];
        if (elevator.State is SosState)
        {
            SetStatus($"{elevator.Name} is in SOS mode. Reset the SOS first.", true);
            return;
        }

        // standing at that floor with nothing to do: the doors just (re)open, no light needed
        bool alreadyHere = floor == elevator.CurrentFloor && elevator.AtFloor &&
                           (elevator.State is IdleState || elevator.State is DoorsOpenState);
        if (!alreadyHere) _carButtons[index, floor].BackColor = LightOn;

        if (elevator.AddRequest(floor))
            _logger.Log(elevator.Id, "Car request", floor, $"Floor button {floor} pressed inside {elevator.Name}");
    }

    private void OnOpenDoor(int index)
    {
        var elevator = _elevators[index];
        if (elevator.OpenDoor())
            _logger.Log(elevator.Id, "Door", elevator.CurrentFloor, "Open-door button pressed");
        else
            SetStatus("Doors can only be opened while the elevator is stopped at a floor.", false);
    }

    private void OnCloseDoor(int index)
    {
        var elevator = _elevators[index];
        if (elevator.CloseDoor())
            _logger.Log(elevator.Id, "Door", elevator.CurrentFloor, "Close-door button pressed");
    }

    private void OnSos(int index)
    {
        var elevator = _elevators[index];

        if (elevator.State is SosState)
        {
            elevator.ResetSos();
            SetStatus($"{elevator.Name}: SOS cleared.", false);
            _logger.Log(elevator.Id, "SOS reset", elevator.CurrentFloor, "Emergency cleared");
            return;
        }

        var pending = elevator.Targets.ToList();
        elevator.TriggerSos();
        for (int f = 0; f < FloorCount; f++) _carButtons[index, f].BackColor = LightOff;

        _logger.Log(elevator.Id, "SOS", elevator.CurrentFloor, $"SOS pressed in {elevator.Name} (position {elevator.Position:0.00})");
        SetStatus($"SOS in {elevator.Name}! Help has been requested.", true);
        try { System.Media.SystemSounds.Hand.Play(); } catch { /* no sound device */ }

        // floor requests that were waiting for this elevator go to the other one
        foreach (int floor in pending.Where(f => _hallButtons[f].BackColor == LightOn))
        {
            var other = _dispatcher.HandleFloorRequest(floor);
            if (other != null)
                _logger.Log(other.Id, "Floor request", floor, $"Request for floor {floor} re-assigned to {other.Name}");
        }
    }

    // ---------- observer callbacks ----------

    public void OnPositionChanged(Elevator e)
    {
        int i = e.Id - 1;
        _shafts[i].Position = e.Position;
        _shafts[i].Invalidate();
        _interiors[i].Floor = e.CurrentFloor;
        _interiors[i].Invalidate();
        UpdateTexts(e);
    }

    public void OnDoorsChanged(Elevator e)
    {
        int i = e.Id - 1;
        _shafts[i].DoorOpenness = e.DoorOpenness;
        _shafts[i].Invalidate();
        _interiors[i].DoorOpenness = e.DoorOpenness;
        _interiors[i].Invalidate();
        UpdateTexts(e);
    }

    public void OnStateChanged(Elevator e)
    {
        RefreshView(e);
        _logger.Log(e.Id, "State", e.CurrentFloor, e.State.Name);
    }

    public void OnArrived(Elevator e, int floor)
    {
        _hallButtons[floor].BackColor = LightOff;                 // lights go out on arrival
        _carButtons[e.Id - 1, floor].BackColor = LightOff;
        _logger.Log(e.Id, "Arrived", floor, $"{e.Name} at floor {floor}; doors open");
    }

    private void RefreshView(Elevator e)
    {
        int i = e.Id - 1;
        _shafts[i].Position = e.Position;
        _shafts[i].DoorOpenness = e.DoorOpenness;
        _shafts[i].Invalidate();

        var view = _interiors[i];
        view.Floor = e.CurrentFloor;
        view.DoorOpenness = e.DoorOpenness;
        view.Arrow = e.State is MovingUpState ? 1 : e.State is MovingDownState ? -1 : 0;
        view.Alarm = e.State is SosState;
        view.Invalidate();

        _sosButtons[i].Text = view.Alarm ? "RESET\r\nSOS" : "SOS";
        _sosButtons[i].BackColor = view.Alarm ? Color.FromArgb(120, 20, 20) : Color.FromArgb(200, 30, 30);
        UpdateTexts(e);
    }

    private void UpdateTexts(Elevator e)
    {
        int i = e.Id - 1;
        string arrow = e.State is MovingUpState ? " ▲" : e.State is MovingDownState ? " ▼" : "";
        _displays[i].Text = e.State is SosState ? "SOS" : $"Floor {e.CurrentFloor}{arrow}";
        _stateLabels[i].Text = e.State.Name;
        _doorTimers[i].Text = e.State is DoorsOpenState doors
            ? $"Door timer: {doors.HoldRemainingSeconds:0.0} s"
            : "Door timer: --";
    }
}
