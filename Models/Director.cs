namespace AuthECAPI.Models
{
    public class Director
    {
        public int DirectorId { get; set; } // Primary key
        public string FullName { get; set; } // Full name of the director
        public string Role { get; set; } // Role (e.g., Mission Director)
    }
}
