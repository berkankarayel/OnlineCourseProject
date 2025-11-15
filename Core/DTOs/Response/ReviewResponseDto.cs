namespace OnlineCourseApi.Core.DTOs.Response;

public class ReviewResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string? UserProfileImageUrl { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
