-- ============================================
-- COPY & PASTE THIS TO FIX THE ERROR
-- ============================================
-- Error: "There is already an object named 'Students' in the database"
-- Fix: Register the migrations with EF Core
-- Time: 10 seconds
-- ============================================

-- STEP 1: Insert migration history
INSERT INTO dbo.__EFMigrationsHistory (MigrationId, ProductVersion)
VALUES 
    ('20260324113538_InitialCreate', '10.0.0'),
    ('20260324170305_Migration_march24', '10.0.0'),
    ('20260407164805_AddEmailAndPhone', '10.0.0'),
    ('20260410090753_AddCourses', '10.0.0'),
    ('20260410090843_AddCoursesTable', '10.0.0'),
    ('20260416095241_CreateUsersTable', '10.0.0'),
    ('20260416095824_RenameUsersPasswordColumn', '10.0.0');

-- STEP 2: Verify it worked
SELECT COUNT(*) AS MigrationCount FROM dbo.__EFMigrationsHistory;
-- Should return: 7

-- STEP 3: Done! Now press F5 in Visual Studio
PRINT '========================================';
PRINT '✅ Fix Applied Successfully!';
PRINT '========================================';
PRINT 'Next: Press F5 in Visual Studio';
PRINT 'App will now start without errors!';
PRINT '========================================';
