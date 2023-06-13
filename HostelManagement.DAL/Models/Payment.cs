using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; } 
        
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string ModeOfPayment { get; set; } 

        [Required]
        public string Status { get; set; } 

        //Foreign key
        [Required]
        public int BookingId { get; set; }

        //Navigation property
        public virtual Booking bookings { get; set; }

    }
}
