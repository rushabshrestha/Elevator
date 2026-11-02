using System.Text.Json;

namespace ElevatorApp;

/// <summary>Reads appsettings.json from the application folder (relative path, no hard-coded drive letters).</summary>
public static class Settings
{
    public const string DefaultConnectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElevatorDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

    public static string ConnectionString { get; private set; } = DefaultConnectionString;

    public static string BaseFolder => AppContext.BaseDirectory;

    public static void Load()
    {
        try
        {
            string path = Path.Combine(BaseFolder, "appsettings.json");
            if (!File.Exists(path)) return;

            using var doc = JsonDocument.Parse(File.ReadAllText(path));
            if (doc.RootElement.TryGetProperty("ConnectionString", out var cs) &&
                !string.IsNullOrWhiteSpace(cs.GetString()))
            {
                ConnectionString = cs.GetString()!;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Could not read appsettings.json: " + ex.Message);
        }
    }
}
