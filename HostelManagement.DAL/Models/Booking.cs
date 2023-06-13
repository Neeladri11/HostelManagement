using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int DurationOfStay { get; set; }

        //Foreign key
        [Required]
        public int StudentId { get; set; }

        //Navigation properties
        public virtual Student students { get; set; }
        public virtual ICollection<Payment> payments { get; set; }

    }
}
