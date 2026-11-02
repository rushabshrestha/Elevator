# Elevator Control System (C# / WinForms / SQL Server)

## Requirements
- Windows 10/11, Visual Studio 2022 ("NET desktop development" workload) or the .NET 8 SDK
- SQL Server (Express / Developer / LocalDB) and SQL Server Management Studio (SSMS)

## Connect to SQL Server (step by step)
1. Open SSMS. In the "Connect to Server" window note **Server name** and **Authentication**
   (examples: `localhost\SQLEXPRESS`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`, `DESKTOP-ABC123`).
2. Click Connect. If it works there, the same name works in the application.
3. Open `appsettings.json` (next to the .exe, or in the project folder before building) and put that name in `ConnectionString`:
   - Windows authentication:
     `Server=localhost\\SQLEXPRESS;Database=ElevatorDB;Trusted_Connection=True;TrustServerCertificate=True;`
   - SQL login (e.g. sa):
     `Server=localhost\\SQLEXPRESS;Database=ElevatorDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;`
   - LocalDB:
     `Server=(localdb)\\MSSQLLocalDB;Database=ElevatorDB;Trusted_Connection=True;TrustServerCertificate=True;`
   (inside JSON every backslash is written twice: `\\`)
4. Start the application. It creates database **ElevatorDB** and the tables automatically
   (`Database\ElevatorDB.sql`; you may also run this script in SSMS yourself). Press "Reconnect database" after editing the file.
5. In SSMS: refresh Databases, then run
   `SELECT * FROM ElevatorDB.dbo.EventLog ORDER BY LogId DESC;`
   If it does not connect: make sure the SQL Server service is running (SQL Server Configuration Manager / Services),
   and for SQL logins that "SQL Server and Windows Authentication mode" is enabled.

## Design view
Open `MainForm.cs` (double-click it in Solution Explorer) to see the form in Visual Studio's Design view. If it shows an error or stays blank, build the project once (Ctrl+Shift+B) and reload the designer. The layout code is in `MainForm.Designer.cs`.

## Using it
- Floor request buttons (left): call an elevator; the one with the shortest estimated response time is sent.
- Control panel (one per elevator): floor buttons 0 / 1, Open door, Close door, SOS.
- Doors (car door and floor door) open on arrival, a door timer counts down, then they close. Doors stay closed while moving.
- SOS: stops that elevator (doors open if it is at a floor), flashes an alarm, logs it to SQL Server and hands waiting
  floor requests to the other elevator. Press RESET SOS to put it back in service.
- Header clock shows the current time; press **View event log** (header) to open the log in its own window. It shows the latest 100 rows of `dbo.EventLog`; select rows and press **Delete selected**, or **Clear all** to empty the log (both ask for confirmation).

## Code map
- `Elevator.cs` State pattern (Idle, MovingUp, MovingDown, DoorsOpen, Sos) + Observer interface
- `Dispatcher.cs` chooses the elevator for floor requests
- `InteriorView.cs` draws `Assets\lift.jpg` with animated sliding doors; `ShaftView.cs` draws the shaft
- `MainForm.Designer.cs` form layout (Design view); `LogForm.cs` / `LogForm.Designer.cs` the separate event-log window; `MainForm.cs`  timer (20 ms), button handlers; `EventLogger.cs` BackgroundWorker logging; `Db.cs` all SQL access
