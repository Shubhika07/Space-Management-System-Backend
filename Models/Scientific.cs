using System.ComponentModel.DataAnnotations;

namespace AuthECAPI.Models
{
    public class ScientificData
    {
        [Key]
        public int DataId { get; set; } // Primary key

        public int MissionId { get; set; } // Foreign key to Mission

        public string ExperimentType { get; set; } // Type of experiment conducted

        public DateTime CollectedDate { get; set; } // Date when data was collected

        public string DataReport { get; set; } // Data report details

        public int ResearcherId { get; set; } // Foreign key to Researcher

    }
}
