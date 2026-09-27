using System;
using System.Linq;
using PIM.Domain.Entities;

namespace PIM.Infrastructure
{
    public static class DevDataSeeder
    {
        public static void EnsureSeedData(AppDbContext db)
        {
            try
            {
                if (db.Courses.Any())
                {
                    Console.WriteLine("[DevDataSeeder] Data already exists. Skipping.");
                    return;
                }

                Console.WriteLine("[DevDataSeeder] Seeding demo data...");

                // Professores (além do admin)
                var t1 = new Teacher { Name = "Prof. Silva", Email = "silva@local", Role = Domain.Enums.Role.Teacher };
                var t2 = new Teacher { Name = "Prof. Souza", Email = "souza@local", Role = Domain.Enums.Role.Teacher };
                db.Teachers.AddRange(t1, t2);

                // Cursos
                var c1 = new Course { Title = "Introdução ao Inglês", Description = "Básico", Level = "Beginner", Teacher = t1 };
                var c2 = new Course { Title = "Gramática Intermediária", Description = "Intermediário", Level = "Intermediate", Teacher = t2 };
                var c3 = new Course { Title = "Vocabulário Avançado", Description = "Avançado", Level = "Advanced", Teacher = t1 };
                db.Courses.AddRange(c1, c2, c3);

                // Lições
                db.Lessons.AddRange(
                    new Lesson { Title = "Lesson 1", Course = c1, Order = 1 },
                    new Lesson { Title = "Lesson 2", Course = c1, Order = 2 },
                    new Lesson { Title = "Lesson 1", Course = c2, Order = 1 },
                    new Lesson { Title = "Lesson 2", Course = c2, Order = 2 },
                    new Lesson { Title = "Lesson 1", Course = c3, Order = 1 },
                    new Lesson { Title = "Lesson 2", Course = c3, Order = 2 }
                );

                // Alunos
                var s1 = new Student { Name = "Aluno A", Email = "alunoa@local", Role = Domain.Enums.Role.Student };
                var s2 = new Student { Name = "Aluno B", Email = "alunob@local", Role = Domain.Enums.Role.Student };
                var s3 = new Student { Name = "Aluno C", Email = "alunoc@local", Role = Domain.Enums.Role.Student };
                var s4 = new Student { Name = "Aluno D", Email = "alunod@local", Role = Domain.Enums.Role.Student };
                db.Students.AddRange(s1, s2, s3, s4);

                db.Enrollments.AddRange(
                    new Enrollment { Student = s1, Course = c1 },
                    new Enrollment { Student = s2, Course = c1 },
                    new Enrollment { Student = s3, Course = c2 },
                    new Enrollment { Student = s4, Course = c3 }
                );

                db.SaveChanges();
                Console.WriteLine("[DevDataSeeder] Demo data seeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DevDataSeeder] Seed failed: " + ex.Message);
            }
        }
    }
}
