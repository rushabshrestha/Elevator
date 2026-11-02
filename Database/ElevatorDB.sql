-- Run this in SQL Server Management Studio (the application also runs it automatically on start-up).
IF DB_ID(N'ElevatorDB') IS NULL
    CREATE DATABASE ElevatorDB;
GO

USE ElevatorDB;
GO

IF OBJECT_ID(N'dbo.Elevators', N'U') IS NULL
    CREATE TABLE dbo.Elevators
    (
        ElevatorId INT          NOT NULL PRIMARY KEY,
        Name       NVARCHAR(50) NOT NULL
    );
GO

IF OBJECT_ID(N'dbo.EventLog', N'U') IS NULL
    CREATE TABLE dbo.EventLog
    (
        LogId      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        EventTime  DATETIME2         NOT NULL DEFAULT SYSDATETIME(),
        ElevatorId INT               NULL REFERENCES dbo.Elevators(ElevatorId),
        EventType  NVARCHAR(50)      NOT NULL,
        Floor      INT               NULL,
        Details    NVARCHAR(200)     NULL
    );
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Elevators WHERE ElevatorId = 1)
    INSERT INTO dbo.Elevators (ElevatorId, Name) VALUES (1, N'Elevator A');
IF NOT EXISTS (SELECT 1 FROM dbo.Elevators WHERE ElevatorId = 2)
    INSERT INTO dbo.Elevators (ElevatorId, Name) VALUES (2, N'Elevator B');
GO

-- Useful check query:
-- SELECT TOP 100 * FROM dbo.EventLog ORDER BY LogId DESC;
