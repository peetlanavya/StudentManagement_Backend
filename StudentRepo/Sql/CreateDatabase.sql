-- ====================================
-- Create StudentDB Database Tables
-- Run this script in SSMS
-- ====================================

-- Drop tables if they exist (for clean setup)
IF OBJECT_ID('dbo.__EFMigrationsHistory', 'U') IS NOT NULL 
    DROP TABLE dbo.__EFMigrationsHistory;

IF OBJECT_ID('dbo.Courses', 'U') IS NOT NULL 
    DROP TABLE dbo.Courses;

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL 
    DROP TABLE dbo.Users;

IF OBJECT_ID('dbo.Students', 'U') IS NOT NULL 
    DROP TABLE dbo.Students;

-- ====================================
-- Create Students Table
-- ====================================
CREATE TABLE dbo.Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX) NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(MAX) NOT NULL DEFAULT '',
    PhoneNumber NVARCHAR(MAX) NOT NULL DEFAULT ''
);

-- ====================================
-- Create Users Table
-- ====================================
CREATE TABLE dbo.Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Users_Role CHECK ([Role] IN ('Admin','User'))
);

-- Create index on UserName
CREATE UNIQUE INDEX IX_Users_UserName ON dbo.Users(UserName);

-- ====================================
-- Create Courses Table
-- ====================================
CREATE TABLE dbo.Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- ====================================
-- Create EF Migrations History Table
-- ====================================
CREATE TABLE dbo.__EFMigrationsHistory (
    MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
    ProductVersion NVARCHAR(32) NOT NULL
);

-- ====================================
-- Insert Initial Data (Optional)
-- ====================================

-- Insert sample students
INSERT INTO dbo.Students (Name, Age, Email, PhoneNumber)
VALUES 
    ('John Doe', 20, 'john@example.com', '1234567890'),
    ('Jane Smith', 21, 'jane@example.com', '9876543210'),
    ('Bob Johnson', 22, 'bob@example.com', '5555555555');

-- Insert default users (passwords are hashed with BCrypt)
-- Password for both accounts: "password"
INSERT INTO dbo.Users (UserName, Password, Role, IsActive)
VALUES 
    ('admin', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'Admin', 1),
    ('user', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'User', 1);

-- Insert sample courses
INSERT INTO dbo.Courses (CourseName, IsActive)
VALUES 
    ('Introduction to C#', 1),
    ('ASP.NET Core Fundamentals', 1),
    ('Entity Framework Core', 1),
    ('Web API Development', 1);

PRINT 'Database tables created successfully!';
PRINT 'Tables created: Students, Users, Courses, __EFMigrationsHistory';
