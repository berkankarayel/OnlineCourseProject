namespace OnlineCourseApi.Core.DTOs.Response;

public class LectureResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string VideoType { get; set; } = string.Empty;
    public int DurationInSeconds { get; set; }
    public int OrderIndex { get; set; }
    public bool IsFree { get; set; }
}
