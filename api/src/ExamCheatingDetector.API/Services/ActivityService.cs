using ExamCheatingDetector.API.DTOs;
using ExamCheatingDetector.API.Models;
using ExamCheatingDetector.API.Repositories;

namespace ExamCheatingDetector.API.Services;

public class ActivityService
{
    private readonly ActivityLogRepository _activityRepo;
    private readonly RiskScoreRepository   _riskScoreRepo;
    private readonly AlertRepository       _alertRepo;

    // Weight per event type
    private static readonly Dictionary<string, decimal> EventWeights = new()
    {
        { "TAB_SWITCH",       3m },
        { "WINDOW_BLUR",      2m },
        { "COPY",             3m },
        { "PASTE",            3m },
        { "RIGHT_CLICK",      1m },
        { "IDLE_TIMEOUT",     4m },
        { "FULLSCREEN_EXIT",  4m }
    };

    public ActivityService(
        ActivityLogRepository activityRepo,
        RiskScoreRepository   riskScoreRepo,
        AlertRepository       alertRepo)
    {
        _activityRepo  = activityRepo;
        _riskScoreRepo = riskScoreRepo;
        _alertRepo     = alertRepo;
    }

    public async Task<ActivityLogResponse> LogActivityAsync(ActivityLogRequest request)
    {
        // 1. Save the activity log
        var log = new ActivityLog
        {
            ExamId      = request.ExamId,
            EventType   = request.EventType.ToUpper(),
            EventDetail = request.EventDetail,
            LoggedAt    = DateTime.UtcNow
        };

        var saved = await _activityRepo.SaveLogAsync(log);

        // 2. Update risk score
        await UpdateRiskScoreAsync(request.ExamId, log.EventType);

        return new ActivityLogResponse
        {
            Id          = saved.Id,
            ExamId      = saved.ExamId,
            EventType   = saved.EventType,
            EventDetail = saved.EventDetail,
            LoggedAt    = saved.LoggedAt
        };
    }

    public async Task<List<ActivityLogResponse>> GetLogsAsync(int examId)
    {
        var logs = await _activityRepo.GetLogsByExamAsync(examId);

        return logs.Select(l => new ActivityLogResponse
        {
            Id          = l.Id,
            ExamId      = l.ExamId,
            EventType   = l.EventType,
            EventDetail = l.EventDetail,
            LoggedAt    = l.LoggedAt
        }).ToList();
    }

    private async Task UpdateRiskScoreAsync(int examId, string eventType)
    {
        var riskScore = await _riskScoreRepo.GetByExamIdAsync(examId);
        if (riskScore == null) return;

        // Add weight for this event
        var weight = EventWeights.GetValueOrDefault(eventType, 1m);
        riskScore.Score += weight;

        await _riskScoreRepo.UpdateScoreAsync(riskScore);

        // 3. Trigger alerts at thresholds
        await CheckAndCreateAlertAsync(examId, riskScore.Score);
    }

    private async Task CheckAndCreateAlertAsync(int examId, decimal score)
    {
        var existingAlerts = await _alertRepo.GetAlertsByExamAsync(examId);

        if (score >= 20)
        {
            bool highExists = existingAlerts
                .Any(a => a.Severity == "High");

            if (!highExists)
            {
                await _alertRepo.CreateAlertAsync(new Alert
                {
                    ExamId    = examId,
                    Message   = $"High cheating risk detected. Score: {score}",
                    Severity  = "High",
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
        else if (score >= 10)
        {
            bool standardExists = existingAlerts
                .Any(a => a.Severity == "Standard");

            if (!standardExists)
            {
                await _alertRepo.CreateAlertAsync(new Alert
                {
                    ExamId    = examId,
                    Message   = $"Suspicious activity detected. Score: {score}",
                    Severity  = "Standard",
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
    }
}
