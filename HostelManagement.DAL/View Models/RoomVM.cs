using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.View_Models
{
    public class RoomVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string RoomStatus { get; set; }

        [Required]
        public int FloorNo { get; set; }

        [Required]
        public int HostelId { get; set; }
    }
}
