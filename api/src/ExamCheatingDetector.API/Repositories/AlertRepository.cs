using ExamCheatingDetector.API.Data;
using ExamCheatingDetector.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamCheatingDetector.API.Repositories;

public class AlertRepository
{
    private readonly AppDbContext _db;

    public AlertRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Alert> CreateAlertAsync(Alert alert)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync();
        return alert;
    }

    public async Task<List<Alert>> GetAlertsByExamAsync(int examId)
    {
        return await _db.Alerts
            .Where(a => a.ExamId == examId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }
}
