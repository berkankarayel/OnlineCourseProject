namespace OnlineCourseApi.Core.Entities;

public class QA
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LectureId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string? Answer { get; set; }
    public bool IsAnswered { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AnsweredAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Lecture Lecture { get; set; } = null!;
}
