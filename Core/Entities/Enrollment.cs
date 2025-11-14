namespace OnlineCourseApi.Core.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public decimal ProgressPercentage { get; set; } = 0;
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Navigation Properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public ICollection<LectureProgress> LectureProgresses { get; set; } = new List<LectureProgress>();
}
