using System.ComponentModel.DataAnnotations;

namespace AuthECAPI.Models
{
    public class EnvironmentData
    {
        [Key]
        public int AssessmentId { get; set; } // Primary key for the environmental assessment entry

        public int MissionId { get; set; } // Foreign key to the Mission table

        [MaxLength(100)]
        public string ImpactType { get; set; } // Type of environmental impact (e.g., Pollution, Debris, Habitat Disruption)

        public DateTime Date { get; set; } // Date of the assessment

        [MaxLength(1000)]
        public string Recommendations { get; set; } // Recommendations to mitigate environmental impact

        [MaxLength(100)]
        public string ConductedBy { get; set; } // Name or ID of the person who conducted the assessment
    }
}
