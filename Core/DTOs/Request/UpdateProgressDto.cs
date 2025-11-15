namespace OnlineCourseApi.Core.DTOs.Request;

public class UpdateProgressDto
{
    public int LectureId { get; set; }
    public bool IsCompleted { get; set; }
    public decimal WatchedPercentage { get; set; }
}
