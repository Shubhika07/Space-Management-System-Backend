using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ScientificData> ScientificData { get; set; }
        public DbSet<ProjectPlan> ProjectPlan { get; set; }
        public DbSet<MissionPerformance> MissionPerformance { get; set; }
        public DbSet<SafetyData> SafetyData { get; set; }
        public DbSet<CostData> CostData { get; set; }
        public DbSet<EnvironmentData> EnvironmentData { get; set; }
        public DbSet<ReportData> ReportData { get; set; }
        public DbSet<NotificationData> NotificationData { get; set; }
    }
}
