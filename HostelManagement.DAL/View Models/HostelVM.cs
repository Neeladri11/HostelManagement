using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.View_Models
{
    public class HostelVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NoOfStudents { get; set; }

        [Required]
        public int NoOfRooms { get; set; }

        [Required]
        public int NoOfAvailableRooms { get; set; }
    }
}
