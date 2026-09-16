using StudentRepo.Data;
using StudentRepo.models;
using System.Linq;

namespace StudentRepo
{
    public static class DbInitializer
    {
        public static void Initialize(StudentDbContext context)
        {
            // ❌ Removed Migrate() — DB already exists

            SeedUsers(context);
        }

        private static void SeedUsers(StudentDbContext context)
        {
            // ✅ DO NOTHING if users already exist
            if (context.Users.Any())
                return;

            context.Users.AddRange(
                new AppUser
                {
                    UserName = "admin",
                    Password = "password",
                    Role = "Admin",
                    IsActive = true
                },
                new AppUser
                {
                    UserName = "user",
                    Password = "password",
                    Role = "User",
                    IsActive = true
                }
            );

            context.SaveChanges();
        }
    }
}