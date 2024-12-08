using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthECAPI.Models
{
    public class SafetyData
    {
        [Key]
        public int LogId { get; set; } // Primary key for the safety log

        public int MissionId { get; set; } // Foreign key to the Mission table

        [MaxLength(500)]
        public string Description { get; set; } // Description of the safety incident or protocol

        public DateTime Date { get; set; } // Date when the safety log was created

        [MaxLength(50)]
        public string Severity { get; set; } // Severity level of the incident (e.g., Low, Medium, High)

        [MaxLength(100)]
        public string ResolutionStatus { get; set; } // Status of resolution (e.g., Resolved, Pending)

        [MaxLength(100)]
        public string ReportedBy { get; set; } // Name or ID of the person who reported the issue
    }
}
