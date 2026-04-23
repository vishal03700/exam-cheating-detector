using ExamCheatingDetector.API.Data;
using ExamCheatingDetector.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamCheatingDetector.API.Repositories;

public class ActivityLogRepository
{
    private readonly AppDbContext _db;

    public ActivityLogRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ActivityLog> SaveLogAsync(ActivityLog log)
    {
        _db.ActivityLogs.Add(log);
        await _db.SaveChangesAsync();
        return log;
    }

    public async Task<List<ActivityLog>> GetLogsByExamAsync(int examId)
    {
        return await _db.ActivityLogs
            .Where(l => l.ExamId == examId)
            .OrderByDescending(l => l.LoggedAt)
            .ToListAsync();
    }
}
