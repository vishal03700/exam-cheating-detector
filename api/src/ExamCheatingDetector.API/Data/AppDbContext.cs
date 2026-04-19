using ExamCheatingDetector.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamCheatingDetector.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<RiskScore> RiskScores { get; set; }
    public DbSet<Alert> Alerts { get; set; }
}
