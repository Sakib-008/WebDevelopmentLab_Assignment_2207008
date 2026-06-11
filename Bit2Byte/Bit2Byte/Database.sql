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
        PendingEmail NVARCHAR(256) NULL,
        PasswordHash NVARCHAR(256) NOT NULL,
        AvatarPath NVARCHAR(260) NULL,
        Bio NVARCHAR(MAX) NULL,
        Interests NVARCHAR(MAX) NULL,
        EmailChangeToken NVARCHAR(100) NULL,
        EmailChangeTokenExpires DATETIME NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        Role NVARCHAR(50) NOT NULL DEFAULT 'member',
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

-- If you already have a Users table and need to add the Role column, run:
IF EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.name='Users' AND s.name='dbo')
BEGIN
    IF COL_LENGTH('dbo.Users','PendingEmail') IS NULL ALTER TABLE dbo.Users ADD PendingEmail NVARCHAR(256) NULL;
    IF COL_LENGTH('dbo.Users','AvatarPath') IS NULL ALTER TABLE dbo.Users ADD AvatarPath NVARCHAR(260) NULL;
    IF COL_LENGTH('dbo.Users','Bio') IS NULL ALTER TABLE dbo.Users ADD Bio NVARCHAR(MAX) NULL;
    IF COL_LENGTH('dbo.Users','Interests') IS NULL ALTER TABLE dbo.Users ADD Interests NVARCHAR(MAX) NULL;
    IF COL_LENGTH('dbo.Users','EmailChangeToken') IS NULL ALTER TABLE dbo.Users ADD EmailChangeToken NVARCHAR(100) NULL;
    IF COL_LENGTH('dbo.Users','EmailChangeTokenExpires') IS NULL ALTER TABLE dbo.Users ADD EmailChangeTokenExpires DATETIME NULL;
    IF COL_LENGTH('dbo.Users','Role') IS NULL
    BEGIN
        ALTER TABLE dbo.Users ADD Role NVARCHAR(50) NOT NULL CONSTRAINT DF_Users_Role DEFAULT 'member';
    END
END

-- Make an existing user an admin (replace email as needed):
-- UPDATE dbo.Users SET Role='admin' WHERE Email='admin@example.com';

-- Notes:
-- 1) Open this file in SSMS and execute. If you use a different SQL Server instance, change the connection in Web.config accordingly.
-- 2) For production, store password hashes (e.g. PBKDF2/BCrypt) rather than plaintext.
