using ExamCheatingDetector.API.DTOs;
using ExamCheatingDetector.API.Repositories;

namespace ExamCheatingDetector.API.Services;

public class ExamService
{
    private readonly ExamRepository _examRepository;

    public ExamService(ExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<ExamResponse> StartExamAsync(int studentId)
    {
        var exam = await _examRepository.StartExamAsync(studentId);

        return new ExamResponse
        {
            Id          = exam.Id,
            StudentId   = exam.StudentId,
            StudentName = string.Empty,
            StartTime   = exam.StartTime,
            EndTime     = exam.EndTime,
            Status      = exam.Status
        };
    }

    public async Task<ExamResponse?> EndExamAsync(int examId, int studentId)
    {
        var exam = await _examRepository.EndExamAsync(examId, studentId);

        if (exam == null) return null;

        return new ExamResponse
        {
            Id          = exam.Id,
            StudentId   = exam.StudentId,
            StudentName = string.Empty,
            StartTime   = exam.StartTime,
            EndTime     = exam.EndTime,
            Status      = exam.Status
        };
    }

    public async Task<ExamResponse?> GetExamAsync(int examId)
    {
        var exam = await _examRepository.GetExamByIdAsync(examId);

        if (exam == null) return null;

        return new ExamResponse
        {
            Id          = exam.Id,
            StudentId   = exam.StudentId,
            StudentName = exam.Student.FullName,
            StartTime   = exam.StartTime,
            EndTime     = exam.EndTime,
            Status      = exam.Status
        };
    }
}
