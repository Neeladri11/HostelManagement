using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; } 
        
        [Required]
        public string RoomStatus { get; set; } 
        
        [Required]
        public int FloorNo { get; set; }
        
        //Foreign Key
        [Required] 
        public int HostelId { get; set; } 

        //Navigation properties
        public virtual Hostel hostels { get; set; }
        public virtual ICollection<Student> students { get; set; }

    }
}
