using System.Data;
using Microsoft.Data.SqlClient;

namespace ElevatorApp;

/// <summary>
/// The only place that opens SQL Server connections. Every database operation in the
/// application goes through <see cref="WithConnection{T}"/>, so there is no duplicated code.
/// </summary>
public static class Db
{
    private static T WithConnection<T>(string connectionString, Func<SqlConnection, T> work)
    {
        using var con = new SqlConnection(connectionString);
        con.Open();
        return work(con);
    }

    public static SqlParameter P(string name, object? value) =>
        new(name, value ?? DBNull.Value);

    public static int Execute(string sql, params SqlParameter[] parameters) =>
        WithConnection(Settings.ConnectionString, con =>
        {
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteNonQuery();
        });

    public static DataTable Query(string sql, params SqlParameter[] parameters) =>
        WithConnection(Settings.ConnectionString, con =>
        {
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        });

    /// <summary>Creates the database and tables from Database\ElevatorDB.sql (batches separated by GO).</summary>
    public static void EnsureCreated()
    {
        string scriptPath = Path.Combine(Settings.BaseFolder, "Database", "ElevatorDB.sql");
        if (!File.Exists(scriptPath))
            throw new FileNotFoundException("Database script not found.", scriptPath);

        var builder = new SqlConnectionStringBuilder(Settings.ConnectionString) { InitialCatalog = "master" };

        var batches = new List<string>();
        var current = new System.Text.StringBuilder();
        foreach (string line in File.ReadAllLines(scriptPath))
        {
            if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
            {
                batches.Add(current.ToString());
                current.Clear();
            }
            else current.AppendLine(line);
        }
        batches.Add(current.ToString());

        WithConnection(builder.ConnectionString, con =>
        {
            foreach (string batch in batches)
            {
                if (string.IsNullOrWhiteSpace(batch)) continue;
                using var cmd = new SqlCommand(batch, con);
                cmd.ExecuteNonQuery();
            }
            return 0;
        });
    }
}
