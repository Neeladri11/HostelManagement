using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } 

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int DurationOfStay { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

    }
}
