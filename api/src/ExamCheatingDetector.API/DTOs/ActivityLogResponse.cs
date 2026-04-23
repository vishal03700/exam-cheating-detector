namespace ExamCheatingDetector.API.DTOs;

public class ActivityLogResponse
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? EventDetail { get; set; }
    public DateTime LoggedAt { get; set; }
}
