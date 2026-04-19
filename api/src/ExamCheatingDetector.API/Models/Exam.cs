namespace ExamCheatingDetector.API.Models;

public class Exam
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "Active";

    public User Student { get; set; } = null!;
    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    public RiskScore? RiskScore { get; set; }
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
