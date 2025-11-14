using Microsoft.EntityFrameworkCore;
using OnlineCourseApi.Core.Entities;

namespace OnlineCourseApi.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<LectureProgress> LectureProgresses { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<QA> QAs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);// Primary Key olduğunu belirtir
            entity.HasIndex(e => e.Email).IsUnique(); // Email alanının benzersiz olduğunu belirtir
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);// Email alanının zorunlu olduğunu ve maksimum uzunluğunu belirtir
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50); // FirstName alanının zorunlu olduğunu ve maksimum uzunluğunu belirtir
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
        });

        // Category Configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Course Configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.ShortDescription).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.Level).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Language).HasMaxLength(50);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Section Configuration
        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Sections)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Lecture Configuration
        modelBuilder.Entity<Lecture>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.VideoType).HasMaxLength(20);

            entity.HasOne(e => e.Section)
                .WithMany(s => s.Lectures)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Attachment Configuration
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FileUrl).IsRequired();
            entity.Property(e => e.FileType).HasMaxLength(50);

            entity.HasOne(e => e.Lecture)
                .WithMany(l => l.Attachments)
                .HasForeignKey(e => e.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Enrollment Configuration
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProgressPercentage).HasPrecision(5, 2);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
        });

        // LectureProgress Configuration
        modelBuilder.Entity<LectureProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WatchedPercentage).HasPrecision(5, 2);

            entity.HasOne(e => e.Enrollment)
                .WithMany(en => en.LectureProgresses)
                .HasForeignKey(e => e.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Lecture)
                .WithMany(l => l.LectureProgresses)
                .HasForeignKey(e => e.LectureId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.EnrollmentId, e.LectureId }).IsUnique();
        });

        // Quiz Configuration
        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.Section)
                .WithMany(s => s.Quizzes)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Question Configuration
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.QuestionText).IsRequired();
            entity.Property(e => e.OptionA).IsRequired().HasMaxLength(500);
            entity.Property(e => e.OptionB).IsRequired().HasMaxLength(500);
            entity.Property(e => e.OptionC).IsRequired().HasMaxLength(500);
            entity.Property(e => e.OptionD).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CorrectAnswer).IsRequired().HasMaxLength(1);

            entity.HasOne(e => e.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(e => e.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // QuizAttempt Configuration
        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Score).HasPrecision(18, 2);
            entity.Property(e => e.TotalScore).HasPrecision(18, 2);
            entity.Property(e => e.Percentage).HasPrecision(5, 2);

            entity.HasOne(e => e.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Quiz)
                .WithMany(q => q.QuizAttempts)
                .HasForeignKey(e => e.QuizId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // UserAnswer Configuration
        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SelectedAnswer).IsRequired().HasMaxLength(1);

            entity.HasOne(e => e.QuizAttempt)
                .WithMany(qa => qa.UserAnswers)
                .HasForeignKey(e => e.QuizAttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Certificate Configuration
        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CertificateCode).IsUnique();
            entity.Property(e => e.CertificateCode).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Certificates)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
        });

        // Review Configuration
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
        });

        // Wishlist Configuration
        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Wishlists)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
        });

        // QA Configuration
        modelBuilder.Entity<QA>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Lecture)
                .WithMany(l => l.Questions)
                .HasForeignKey(e => e.LectureId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
