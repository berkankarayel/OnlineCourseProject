namespace OnlineCourseApi.Core.DTOs.Request;

public class CreateLectureDto
{
    public int SectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string VideoType { get; set; } = "uploaded"; // youtube, uploaded, vimeo
    public int DurationInSeconds { get; set; }
    public int OrderIndex { get; set; }
    public bool IsFree { get; set; } = false;
}
