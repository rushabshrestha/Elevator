using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ElevatorApp;

/// <summary>Delegate used to report logging problems back to the GUI.</summary>
public delegate void LoggerErrorHandler(string message);

/// <summary>
/// Writes events to SQL Server and reloads the log on BackgroundWorker threads,
/// so the GUI (and the animation) never blocks on the database.
/// </summary>
public class EventLogger
{
    private bool _refreshing;
    private bool _refreshPending;

    public bool Enabled { get; set; }
    public event Action<DataTable>? LogLoaded;
    public event LoggerErrorHandler? Failed;
    public event Action<int>? Deleted;                 // number of rows removed
    public event LoggerErrorHandler? DeleteFailed;

    public void Log(int? elevatorId, string eventType, int? floor, string details)
    {
        if (!Enabled) return;

        var worker = new BackgroundWorker();
        worker.DoWork += (_, e) =>
        {
            try
            {
                Db.Execute(
                    "INSERT INTO dbo.EventLog (ElevatorId, EventType, Floor, Details) VALUES (@e, @t, @f, @d)",
                    Db.P("@e", elevatorId), Db.P("@t", eventType), Db.P("@f", floor), Db.P("@d", details));
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        };
        worker.RunWorkerCompleted += (_, e) =>
        {
            if (e.Result is Exception ex) Fail(ex);
            else Refresh();
        };
        worker.RunWorkerAsync();
    }

    /// <summary>Deletes the given log rows (by LogId) on a BackgroundWorker.</summary>
    public void Delete(IReadOnlyList<int> ids)
    {
        if (ids.Count == 0) return;
        string names = string.Join(",", ids.Select((_, i) => "@id" + i));
        var parameters = ids.Select((id, i) => Db.P("@id" + i, id)).ToArray();
        RunDelete("DELETE FROM dbo.EventLog WHERE LogId IN (" + names + ")", parameters);
    }

    /// <summary>Deletes every log row.</summary>
    public void Clear() => RunDelete("DELETE FROM dbo.EventLog");

    private void RunDelete(string sql, params SqlParameter[] parameters)
    {
        var worker = new BackgroundWorker();
        worker.DoWork += (_, e) =>
        {
            try { e.Result = Db.Execute(sql, parameters); }
            catch (Exception ex) { e.Result = ex; }
        };
        worker.RunWorkerCompleted += (_, e) =>
        {
            if (e.Result is Exception ex)
            {
                DeleteFailed?.Invoke(ex.Message);
                return;
            }
            Deleted?.Invoke(e.Result is int rows ? rows : 0);
            Refresh();
        };
        worker.RunWorkerAsync();
    }

    public void Refresh()
    {
        if (!Enabled || LogLoaded == null) return;     // nobody is showing the log
        if (_refreshing) { _refreshPending = true; return; }
        _refreshing = true;

        var worker = new BackgroundWorker();
        worker.DoWork += (_, e) =>
        {
            try
            {
                e.Result = Db.Query(
                    "SELECT TOP 100 l.LogId AS [No], l.EventTime AS [Time], e.Name AS [Elevator], " +
                    "l.EventType AS [Event], l.Floor AS [Floor], l.Details AS [Details] " +
                    "FROM dbo.EventLog l LEFT JOIN dbo.Elevators e ON e.ElevatorId = l.ElevatorId " +
                    "ORDER BY l.LogId DESC");
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        };
        worker.RunWorkerCompleted += (_, e) =>
        {
            _refreshing = false;
            if (e.Result is DataTable table) LogLoaded?.Invoke(table);
            else if (e.Result is Exception ex) Fail(ex);

            if (_refreshPending)
            {
                _refreshPending = false;
                Refresh();
            }
        };
        worker.RunWorkerAsync();
    }

    private void Fail(Exception ex)
    {
        Enabled = false;
        Failed?.Invoke(ex.Message);
    }
}
