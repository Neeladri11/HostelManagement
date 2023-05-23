using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Room
    {
        [Key]
        public string RoomId { get; set; } = string.Empty;
        
        [Required]
        public string RoomStatus { get; set; } = string.Empty;
        
        [Required]
        public int FloorNo { get; set; }
        
        [Required] 
        public string HostelId { get; set; } = string.Empty;
        [ForeignKey("HostelId")]
        public virtual Hostel Hostel { get; set; }
    }
}
