using ExamCheatingDetector.API.DTOs;
using ExamCheatingDetector.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamCheatingDetector.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ActivityController : ControllerBase
{
    private readonly ActivityService _activityService;

    public ActivityController(ActivityService activityService)
    {
        _activityService = activityService;
    }

    // POST api/activity/log
    [HttpPost("log")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> LogActivity([FromBody] ActivityLogRequest request)
    {
        if (request.ExamId <= 0 || string.IsNullOrWhiteSpace(request.EventType))
            return BadRequest(new { message = "ExamId and EventType are required." });

        var result = await _activityService.LogActivityAsync(request);
        return Ok(result);
    }

    // GET api/activity/{examId}
    [HttpGet("{examId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLogs(int examId)
    {
        var logs = await _activityService.GetLogsAsync(examId);
        return Ok(logs);
    }
}
