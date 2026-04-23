using ExamCheatingDetector.API.Data;
using ExamCheatingDetector.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamCheatingDetector.API.Repositories;

public class RiskScoreRepository
{
    private readonly AppDbContext _db;

    public RiskScoreRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<RiskScore?> GetByExamIdAsync(int examId)
    {
        return await _db.RiskScores
            .FirstOrDefaultAsync(r => r.ExamId == examId);
    }

    public async Task UpdateScoreAsync(RiskScore riskScore)
    {
        riskScore.UpdatedAt = DateTime.UtcNow;
        _db.RiskScores.Update(riskScore);
        await _db.SaveChangesAsync();
    }
}
