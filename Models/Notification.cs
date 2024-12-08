using System.ComponentModel.DataAnnotations;
namespace AuthECAPI.Models
{
    public class NotificationData
    {
        [Key]
        public int NotificationId { get; set; } // Primary key for the notification entry
        [MaxLength(100)]
        public string Type { get; set; } // Type of notification (e.g., Maintenance Alert, Safety Incident, Mission Milestone)
        public DateTime Timestamp { get; set; } // Timestamp when the notification was generated
        [MaxLength(500)]
        public string Message { get; set; } // Notification message content
        public int UserId { get; set; } // Foreign key to associate the notification with a user
    }
}
