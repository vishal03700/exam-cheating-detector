namespace ExamCheatingDetector.API.Models;

public class RiskScore
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public decimal Score { get; set; } = 0;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Exam Exam { get; set; } = null!;
}
