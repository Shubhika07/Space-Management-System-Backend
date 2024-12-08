namespace AuthECAPI.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; } // Unique identifier for the equipment
        public string Name { get; set; } // Name of the equipment
        public string Type { get; set; } // Type or category of the equipment
        public string Condition { get; set; } // Current condition or status of the equipment (e.g., New, Operational, Damaged)
        public string AssignedMission { get; set; } // Name or ID of the mission to which the equipment is assigned
        public DateTime MaintenanceDate { get; set; } // Date of the last or next maintenance check
    }
}
