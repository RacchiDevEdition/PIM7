using PIM.Infrastructure;
using PIM.Domain.Entities;
using System.Linq;

namespace PIM.Infrastructure
{
    public static class AdminSeeder
    {
        public static void EnsureAdmin(AppDbContext db, PIM.Models.Interfaces.IUserService userService)
        {
            const string adminEmail = "admin@local";
            Console.WriteLine("[AdminSeeder] Starting admin seeding...");
            try
            {
                // verifica por email explicitamente
                if (db.Set<User>().Any(u => u.Email == adminEmail))
                {
                    Console.WriteLine("[AdminSeeder] Admin already exists (by email). Skipping.");
                    return;
                }

                var admin = new Teacher
                {
                    Name = "admin",
                    Email = adminEmail,
                    Role = Domain.Enums.Role.Admin
                };

                var created = userService.Create(admin, "admin123");
                Console.WriteLine($"[AdminSeeder] Created user id={created?.Id}");
                Console.WriteLine("[AdminSeeder] Admin user created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AdminSeeder] Exception during seeding: " + ex.Message);
                try
                {
                    Console.WriteLine("[AdminSeeder] Attempting EnsureCreated() and retry...");
                    db.Database.EnsureCreated();
                    if (!db.Set<User>().Any(u => u.Email == adminEmail))
                    {
                        var admin = new Teacher
                        {
                            Name = "admin",
                            Email = adminEmail,
                            Role = Domain.Enums.Role.Admin
                        };

                        userService.Create(admin, "admin123");
                        Console.WriteLine("[AdminSeeder] Admin user created after EnsureCreated().");
                    }
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("[AdminSeeder] Failed to seed admin after EnsureCreated: " + ex2.Message);
                }
            }
        }
    }
}
