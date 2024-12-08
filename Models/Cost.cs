using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthECAPI.Models
{
    public class CostData
    {
        [Key]
        public int CostId { get; set; } // Primary key for the cost entry

        public int MissionId { get; set; } // Foreign key to the Mission table

        [MaxLength(100)]
        public string ExpenseType { get; set; } // Type of expense (e.g., Equipment, Personnel, Research)

        public DateTime Date { get; set; } // Date of the expense

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } // Cost amount, stored as a decimal for precision

        [MaxLength(100)]
        public string ResponsiblePerson { get; set; } // Name or ID of the person responsible for the expense
    }
}
