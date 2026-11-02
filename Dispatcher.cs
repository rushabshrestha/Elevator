namespace ElevatorApp;

/// <summary>
/// Assigns requests made outside the elevators (floor request buttons) to the elevator with the
/// shortest estimated response time. Existing targets are never removed, so ongoing trips are not disrupted.
/// Elevators in SOS mode are skipped.
/// </summary>
public class Dispatcher
{
    private readonly IReadOnlyList<Elevator> _elevators;

    public Dispatcher(IReadOnlyList<Elevator> elevators) => _elevators = elevators;

    /// <returns>The elevator that will come, or null when no elevator is available.</returns>
    public Elevator? HandleFloorRequest(int floor)
    {
        var available = _elevators.Where(e => e.State is not SosState).ToList();
        if (available.Count == 0) return null;

        // already on its way (or standing here): keep the same elevator
        var chosen = available.FirstOrDefault(e =>
                         e.Targets.Contains(floor) ||
                         (e.State is DoorsOpenState && e.AtFloor && e.CurrentFloor == floor))
                     ?? available.OrderBy(e => e.EstimateResponse(floor)).ThenBy(e => e.Id).First();

        chosen.AddRequest(floor);
        return chosen;
    }
}
