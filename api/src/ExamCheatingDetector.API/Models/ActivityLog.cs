namespace ExamCheatingDetector.API.Models;

public class ActivityLog
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? EventDetail { get; set; }
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public Exam Exam { get; set; } = null!;
}
