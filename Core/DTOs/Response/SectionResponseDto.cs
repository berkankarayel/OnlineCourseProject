namespace OnlineCourseApi.Core.DTOs.Response;

public class SectionResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public List<LectureResponseDto> Lectures { get; set; } = new();
}
