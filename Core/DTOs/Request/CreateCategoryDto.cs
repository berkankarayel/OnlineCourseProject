namespace OnlineCourseApi.Core.DTOs.Request;

public class CreateCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
