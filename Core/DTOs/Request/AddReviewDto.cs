namespace OnlineCourseApi.Core.DTOs.Request;

public class AddReviewDto
{
    public int CourseId { get; set; }
    public int Rating { get; set; } // 1-5
    public string? Comment { get; set; }
}
