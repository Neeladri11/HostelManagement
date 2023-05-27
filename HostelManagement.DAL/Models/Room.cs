using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; } 
        
        [Required]
        public string RoomStatus { get; set; } 
        
        [Required]
        public int FloorNo { get; set; }
        
        [Required] 
        public int HostelId { get; set; } 
        [ForeignKey("HostelId")]
        public virtual Hostel Hostel { get; set; }
    }
}
