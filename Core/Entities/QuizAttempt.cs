namespace OnlineCourseApi.Core.Entities;

public class QuizAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public decimal Score { get; set; }
    public decimal TotalScore { get; set; }
    public decimal Percentage { get; set; }
    public bool IsPassed { get; set; } = false;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime CompletedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Quiz Quiz { get; set; } = null!;
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
