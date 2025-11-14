namespace OnlineCourseApi.Core.Entities;

public class Attachment
{
    public int Id { get; set; }
    public int LectureId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty; // pdf, zip, doc
    public long FileSizeInBytes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Lecture Lecture { get; set; } = null!;
}
