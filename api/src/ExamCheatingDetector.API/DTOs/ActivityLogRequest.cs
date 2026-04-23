namespace ExamCheatingDetector.API.DTOs;

public class ActivityLogRequest
{
    public int ExamId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? EventDetail { get; set; }
}
