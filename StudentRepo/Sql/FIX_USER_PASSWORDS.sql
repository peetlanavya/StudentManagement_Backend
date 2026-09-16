-- ====================================
-- Fix: Reset User Passwords with Correct Hash
-- ====================================
-- Password: "password" (hashed with BCrypt)
-- Run this if login fails with "Invalid user credentials"

-- Delete existing users and re-insert with correct password hash
DELETE FROM dbo.Users;

INSERT INTO dbo.Users (UserName, Password, Role, IsActive)
VALUES 
    ('admin', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'Admin', 1),
    ('user', '$2a$11$slYQmyNdGzin7olVMyCueOK0jHExLQQf8/LewY5Z5MR0yd0dHPtt6', 'User', 1);

PRINT 'Users updated successfully!';
PRINT 'Username: admin, Password: password';
PRINT 'Username: user, Password: password';
