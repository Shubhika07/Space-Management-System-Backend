namespace AuthECAPI.Models
{
    public class Mission
    {
        public int MissionId { get; set; } // Primary key

        public string Name { get; set; } // Mission name

        public string Objective { get; set; } // Mission objective

        public DateTime LaunchDate { get; set; } // Launch date

        public string Status { get; set; } // Status (Active, Completed, etc.)

        public string AssignedTeam { get; set; } // Team assigned to the mission

        public int DirectorId { get; set; } // Foreign key to Director (Assuming Director is another entity)
    }
}
