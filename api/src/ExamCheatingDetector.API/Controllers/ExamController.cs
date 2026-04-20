using System.Security.Claims;
using ExamCheatingDetector.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamCheatingDetector.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamController : ControllerBase
{
    private readonly ExamService _examService;

    public ExamController(ExamService examService)
    {
        _examService = examService;
    }

    // POST api/exam/start
    [HttpPost("start")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> StartExam()
    {
        var studentId = GetStudentId();
        if (studentId == 0)
            return Unauthorized(new { message = "Invalid token." });

        var exam = await _examService.StartExamAsync(studentId);
        return Ok(exam);
    }

    // PATCH api/exam/{examId}/end
    [HttpPatch("{examId}/end")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> EndExam(int examId)
    {
        var studentId = GetStudentId();
        if (studentId == 0)
            return Unauthorized(new { message = "Invalid token." });

        var exam = await _examService.EndExamAsync(examId, studentId);

        if (exam == null)
            return NotFound(new { message = "Exam not found or already completed." });

        return Ok(exam);
    }

    // GET api/exam/{examId}
    [HttpGet("{examId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetExam(int examId)
    {
        var exam = await _examService.GetExamAsync(examId);

        if (exam == null)
            return NotFound(new { message = "Exam not found." });

        return Ok(exam);
    }

    private int GetStudentId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(claim, out var id) ? id : 0;
    }
}
