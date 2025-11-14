namespace OnlineCourseApi.Core.Entities;

public class Certificate
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string CertificateCode { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public string? CertificateUrl { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
