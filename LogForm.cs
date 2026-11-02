using System.Data;

namespace ElevatorApp;

/// <summary>Separate window that shows the SQL Server event log and lets the user delete rows.</summary>
public partial class LogForm : Form
{
    private EventLogger? _logger;

    public LogForm()                       // used by Visual Studio's Design view
    {
        InitializeComponent();
    }

    public LogForm(EventLogger logger) : this()
    {
        _logger = logger;

        grid.DataBindingComplete += (_, _) =>
        {
            if (grid.Columns.Contains("Time")) grid.Columns["Time"]!.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            if (grid.Columns.Contains("No")) grid.Columns["No"]!.FillWeight = 30;
            if (grid.Columns.Contains("Floor")) grid.Columns["Floor"]!.FillWeight = 30;
        };

        btnRefresh.Click += (_, _) => Reload();
        btnDeleteSelected.Click += (_, _) => OnDeleteSelected();
        btnClearLog.Click += (_, _) => OnClearLog();

        Load += (_, _) =>
        {
            _logger.LogLoaded += ShowTable;
            _logger.Deleted += OnDeleted;
            _logger.DeleteFailed += OnDeleteFailed;
            Reload();
        };
        FormClosed += (_, _) =>
        {
            _logger.LogLoaded -= ShowTable;
            _logger.Deleted -= OnDeleted;
            _logger.DeleteFailed -= OnDeleteFailed;
        };
    }

    private void Reload()
    {
        if (_logger == null) return;
        if (!_logger.Enabled)
        {
            SetStatus("Not connected to SQL Server - use Reconnect database in the main window.", true);
            return;
        }
        SetStatus("Loading...", false);
        _logger.Refresh();
    }

    private void ShowTable(DataTable table)
    {
        grid.DataSource = table;
        SetStatus($"Showing the latest {table.Rows.Count} log row(s).", false);
    }

    private void OnDeleted(int count) => SetStatus($"Deleted {count} log row(s).", false);

    private void OnDeleteFailed(string message) => SetStatus("Delete failed: " + message, true);

    private void SetStatus(string text, bool error)
    {
        lblStatus.Text = text;
        lblStatus.ForeColor = error ? Color.Firebrick : Color.DimGray;
    }

    private void OnDeleteSelected()
    {
        if (_logger == null) return;
        if (!_logger.Enabled)
        {
            SetStatus("Not connected to SQL Server - use Reconnect database in the main window.", true);
            return;
        }

        var ids = grid.SelectedRows
            .Cast<DataGridViewRow>()
            .Select(row => row.Cells["No"].Value)
            .OfType<int>()
            .ToList();

        if (ids.Count == 0)
        {
            SetStatus("Select one or more rows first (click a row, Ctrl or Shift for several).", false);
            return;
        }

        var answer = MessageBox.Show(this, $"Delete {ids.Count} selected log row(s)?", "Delete log rows",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (answer == DialogResult.Yes) _logger.Delete(ids);
    }

    private void OnClearLog()
    {
        if (_logger == null) return;
        if (!_logger.Enabled)
        {
            SetStatus("Not connected to SQL Server - use Reconnect database in the main window.", true);
            return;
        }

        var answer = MessageBox.Show(this, "Delete ALL rows from the event log? This cannot be undone.", "Clear log",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        if (answer == DialogResult.Yes) _logger.Clear();
    }
}
