-- ====================================
-- QUICK SETUP SCRIPT FOR SSMS
-- Copy and paste this entire script into SSMS Query Window
-- Make sure StudentDB is selected before running
-- ====================================

-- Step 1: Create Tables
CREATE TABLE dbo.Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX) NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(MAX) NOT NULL DEFAULT '',
    PhoneNumber NVARCHAR(MAX) NOT NULL DEFAULT ''
);

CREATE TABLE dbo.Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Users_Role CHECK ([Role] IN ('Admin','User'))
);

CREATE UNIQUE INDEX IX_Users_UserName ON dbo.Users(UserName);

CREATE TABLE dbo.Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE dbo.__EFMigrationsHistory (
    MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
    ProductVersion NVARCHAR(32) NOT NULL
);

-- Step 2: Create Stored Procedures
CREATE OR ALTER PROCEDURE sp_AddStudent
    @Name NVARCHAR(100),
    @Age INT,
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20)
AS
BEGIN
    INSERT INTO Students(Name, Age, Email, PhoneNumber)
    VALUES(@Name, @Age, @Email, @PhoneNumber)
END

CREATE OR ALTER PROCEDURE sp_GetStudents
AS
BEGIN
    SELECT * FROM Students
END

CREATE OR ALTER PROCEDURE sp_GetStudentById
    @Id INT
AS
BEGIN
    SELECT Id, Name, Age, Email, PhoneNumber
    FROM Students
    WHERE Id = @Id
END

CREATE OR ALTER PROCEDURE sp_UpdateStudent
    @Id INT,
    @Name NVARCHAR(100) = NULL,
    @Age INT = NULL,
    @Email NVARCHAR(100) = NULL,
    @PhoneNumber NVARCHAR(20) = NULL
AS
BEGIN
    UPDATE Students
    SET 
        Name = ISNULL(@Name, Name),
        Age = ISNULL(@Age, Age),
        Email = ISNULL(@Email, Email),
        PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber)
    WHERE Id = @Id
END

CREATE OR ALTER PROCEDURE sp_DeleteStudent
    @Id INT
AS
BEGIN
    DELETE FROM Students
    WHERE Id = @Id
END

-- Step 3: Insert Sample Data
INSERT INTO dbo.Students (Name, Age, Email, PhoneNumber)
VALUES 
    ('John Doe', 20, 'john@example.com', '1234567890'),
    ('Jane Smith', 21, 'jane@example.com', '9876543210'),
    ('Bob Johnson', 22, 'bob@example.com', '5555555555');

INSERT INTO dbo.Users (UserName, Password, Role, IsActive)
VALUES 
    ('admin', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'Admin', 1),
    ('user', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'User', 1);

INSERT INTO dbo.Courses (CourseName, IsActive)
VALUES 
    ('Introduction to C#', 1),
    ('ASP.NET Core Fundamentals', 1),
    ('Entity Framework Core', 1),
    ('Web API Development', 1);

-- Completion message
PRINT '========================================';
PRINT 'Database Setup Complete!';
PRINT '========================================';
PRINT 'Tables created:';
PRINT '  - Students';
PRINT '  - Users';
PRINT '  - Courses';
PRINT '  - __EFMigrationsHistory';
PRINT '';
PRINT 'Stored Procedures created:';
PRINT '  - sp_AddStudent';
PRINT '  - sp_GetStudents';
PRINT '  - sp_GetStudentById';
PRINT '  - sp_UpdateStudent';
PRINT '  - sp_DeleteStudent';
PRINT '';
PRINT 'Sample data inserted!';
PRINT '========================================';
