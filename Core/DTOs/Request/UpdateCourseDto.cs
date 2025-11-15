namespace OnlineCourseApi.Core.DTOs.Request;

public class UpdateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Draft, Published
}
