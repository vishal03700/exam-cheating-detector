namespace ExamCheatingDetector.API.Models;

public class Alert
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool IsAcknowledged { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Exam Exam { get; set; } = null!;
}
