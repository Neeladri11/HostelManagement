using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.Models
{
    public class Hostel
    {
        [Key]
        public string HostelId { get; set; } = string.Empty;

        [Required]
        public int NoOfStudents { get; set; }

        [Required]
        public int NoOfRooms { get; set; }

        [Required]
        public int NoOfAvailableRooms { get; set; }

    }
}