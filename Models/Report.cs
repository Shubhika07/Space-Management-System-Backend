using System.ComponentModel.DataAnnotations;

namespace AuthECAPI.Models
{
    public class ReportData
    {
        [Key]
        public int ReportId { get; set; } // Primary key for the report entry

        [MaxLength(100)]
        public string Type { get; set; } // Type of report (e.g., Mission Progress, Cost Analysis, Safety Compliance)

        public DateTime GeneratedDate { get; set; } // Date when the report was generated

        [MaxLength(5000)]
        public string Data { get; set; } // The content or data of the report

        [MaxLength(100)]
        public string CreatedBy { get; set; } // Name or ID of the person who created the report
    }
}
