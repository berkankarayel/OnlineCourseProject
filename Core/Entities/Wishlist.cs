namespace OnlineCourseApi.Core.Entities;

public class Wishlist
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
