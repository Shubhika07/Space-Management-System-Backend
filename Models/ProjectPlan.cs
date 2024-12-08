using System.ComponentModel.DataAnnotations;

namespace AuthECAPI.Models
{
   public class ProjectPlan
    {
        [Key]
        public int PlanId { get; set; } // Primary key

        public int MissionId { get; set; } // Foreign key to Mission

        public string ActivityType { get; set; } // Type of activity planned

        public DateTime StartDate { get; set; } // Start date of the project activity

        public DateTime EndDate { get; set; } // End date of the project activity

        public string AssignedEngineer { get; set; } // Engineer assigned to the activity

        public string Status { get; set; } // Status of the project activity (e.g., Planned, In Progress, Completed)

    }

}
