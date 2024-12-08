using System;
using System.ComponentModel.DataAnnotations;

namespace AuthECAPI.Models
{
    public class MissionPerformance
    {
        [Key]
        public int PerformanceId { get; set; } // Primary key

        public int MissionId { get; set; } // Foreign key to Mission

        public string ObjectivesMet { get; set; } // Objectives met during the mission

        public string DataCollected { get; set; } // Summary of data collected

        public DateTime CompletionDate { get; set; } // Mission completion date

        public string Supervisor { get; set; } // Name of the mission supervisor

    }
}
