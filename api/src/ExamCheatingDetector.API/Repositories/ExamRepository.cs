using ExamCheatingDetector.API.Data;
using ExamCheatingDetector.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamCheatingDetector.API.Repositories;

public class ExamRepository
{
    private readonly AppDbContext _db;

    public ExamRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Exam> StartExamAsync(int studentId)
    {
        var exam = new Exam
        {
            StudentId = studentId,
            StartTime = DateTime.UtcNow,
            Status    = "Active"
        };

        _db.Exams.Add(exam);
        await _db.SaveChangesAsync();

        var riskScore = new RiskScore
        {
            ExamId    = exam.Id,
            Score     = 0,
            UpdatedAt = DateTime.UtcNow
        };

        _db.RiskScores.Add(riskScore);
        await _db.SaveChangesAsync();

        return exam;
    }

    public async Task<Exam?> EndExamAsync(int examId, int studentId)
    {
        var exam = await _db.Exams
            .FirstOrDefaultAsync(e => e.Id == examId
                                   && e.StudentId == studentId
                                   && e.Status == "Active");

        if (exam == null) return null;

        exam.EndTime = DateTime.UtcNow;
        exam.Status  = "Completed";

        await _db.SaveChangesAsync();
        return exam;
    }

    public async Task<Exam?> GetExamByIdAsync(int examId)
    {
        return await _db.Exams
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.Id == examId);
    }
}
