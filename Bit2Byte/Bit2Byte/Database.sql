-- Database.sql
-- Run this script in SQL Server Management Studio (SSMS) to create the Bit2ByteDB and required tables.

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Bit2ByteDB')
BEGIN
    CREATE DATABASE Bit2ByteDB;
END
GO

USE Bit2ByteDB;
GO

-- Users table (create only if missing)
IF OBJECT_ID('dbo.Users','U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Email NVARCHAR(256) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- Events table (create only if missing)
IF OBJECT_ID('dbo.Events','U') IS NULL
BEGIN
    CREATE TABLE dbo.Events (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Description NVARCHAR(MAX) NULL,
        EventDate DATETIME NOT NULL,
        CreatedByUserId INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Events_Users FOREIGN KEY (CreatedByUserId) REFERENCES dbo.Users(Id)
    );
END
GO

-- Sample inserts (optional) - replace password hashes before use
-- INSERT INTO dbo.Users (Username, Email, PasswordHash) VALUES ('admin','admin@example.com','<replace-with-hash>');
-- INSERT INTO dbo.Events (Title, Description, EventDate, CreatedByUserId) VALUES ('Launch','Project launch','2026-06-01',1);

-- Notes:
-- 1) Open this file in SSMS and execute. If you use a different SQL Server instance, change the connection in Web.config accordingly.
-- 2) For production, store password hashes (e.g. PBKDF2/BCrypt) rather than plaintext.
