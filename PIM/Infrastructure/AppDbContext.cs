using Microsoft.EntityFrameworkCore;
using PIM.Domain.Entities;

namespace PIM.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<LessonContent> LessonContents { get; set; } = null!;
        public DbSet<VideoContent> VideoContents { get; set; } = null!;
        public DbSet<ArticleContent> ArticleContents { get; set; } = null!;
        public DbSet<QuizContent> QuizContents { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Choice> Choices { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Progress> Progresses { get; set; } = null!;
        public DbSet<QuizResult> QuizResults { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Herança TPH para LessonContent
            modelBuilder.Entity<LessonContent>()
                .HasDiscriminator<string>("ContentType")
                .HasValue<VideoContent>("Video")
                .HasValue<ArticleContent>("Article")
                .HasValue<QuizContent>("Quiz");

            // Course - Lesson 1:N
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course -> Teacher: evitar cascade para prevenir múltiplos caminhos de cascade
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CoursesCreated)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lesson - Contents 1:N (FK shadow)
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Contents)
                .WithOne()
                .HasForeignKey("LessonId")
                .OnDelete(DeleteBehavior.Cascade);

            // Student - Enrollment 1:N (sem cascade para evitar multiple cascade paths)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress -> Student: evitar cascade
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Student)
                .WithMany(s => s.ProgressRecords)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // QuizResult -> Student: evitar cascade
            modelBuilder.Entity<QuizResult>()
                .HasOne(q => q.Student)
                .WithMany()
                .HasForeignKey(q => q.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure string lengths as example
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(200);
        }
    }
}
