namespace OnlineCourseApi.Core.DTOs.Response;

public class CourseDetailResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public decimal Price { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    // Category
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    
    // Sections + Lectures
    public List<SectionResponseDto> Sections { get; set; } = new();
    
    // Statistics
    public int TotalLectures { get; set; }
    public int TotalDurationInSeconds { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
}
