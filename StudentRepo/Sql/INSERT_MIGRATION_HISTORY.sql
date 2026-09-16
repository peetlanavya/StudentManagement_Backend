-- ====================================
-- Fix: Insert EF Core Migration History
-- ====================================
-- This tells EF Core that migrations have already been applied
-- Run this AFTER running QUICK_SETUP.sql but BEFORE starting the app

INSERT INTO dbo.__EFMigrationsHistory (MigrationId, ProductVersion)
VALUES 
    ('20260324113538_InitialCreate', '10.0.0'),
    ('20260324170305_Migration_march24', '10.0.0'),
    ('20260407164805_AddEmailAndPhone', '10.0.0'),
    ('20260410090753_AddCourses', '10.0.0'),
    ('20260410090843_AddCoursesTable', '10.0.0'),
    ('20260416095241_CreateUsersTable', '10.0.0'),
    ('20260416095824_RenameUsersPasswordColumn', '10.0.0');

PRINT 'Migration history inserted successfully!';
PRINT 'EF Core will now recognize that all migrations have been applied.';

-- Verify
SELECT MigrationId, ProductVersion FROM dbo.__EFMigrationsHistory ORDER BY MigrationId;
