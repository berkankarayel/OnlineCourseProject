namespace OnlineCourseApi.Core.Entities;

public class Lecture
{
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string VideoType { get; set; } = "uploaded"; // youtube, uploaded, vimeo
    public int DurationInSeconds { get; set; }
    public int OrderIndex { get; set; }
    public bool IsFree { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Section Section { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public ICollection<LectureProgress> LectureProgresses { get; set; } = new List<LectureProgress>();
    public ICollection<QA> Questions { get; set; } = new List<QA>();
}
