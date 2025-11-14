namespace OnlineCourseApi.Core.Entities;

public class UserAnswer
{
    public int Id { get; set; }
    public int QuizAttemptId { get; set; }
    public int QuestionId { get; set; }
    public string SelectedAnswer { get; set; } = string.Empty; // A, B, C, D
    public bool IsCorrect { get; set; } = false;

    // Navigation Properties
    public QuizAttempt QuizAttempt { get; set; } = null!;
    public Question Question { get; set; } = null!;
}
