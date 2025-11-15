namespace OnlineCourseApi.Core.DTOs.Response;

public class CertificateResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string CertificateCode { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public string? CertificateUrl { get; set; }
}
