namespace OnlineCourseApi.Core.DTOs.Response;

public class EnrollmentResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string? CourseThumbnailUrl { get; set; }
    public DateTime EnrolledAt { get; set; }
    public decimal ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}
