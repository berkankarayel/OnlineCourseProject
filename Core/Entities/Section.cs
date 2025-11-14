namespace OnlineCourseApi.Core.Entities;

public class Section
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Course Course { get; set; } = null!;
    public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
