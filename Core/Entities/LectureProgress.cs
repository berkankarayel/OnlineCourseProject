namespace OnlineCourseApi.Core.Entities;

public class LectureProgress
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public int LectureId { get; set; }
    public bool IsCompleted { get; set; } = false;
    public decimal WatchedPercentage { get; set; } = 0;
    public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Enrollment Enrollment { get; set; } = null!;
    public Lecture Lecture { get; set; } = null!;
}
