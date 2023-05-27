using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; } 
        
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string ModeOfPayment { get; set; } 

        [Required]
        public string Status { get; set; } 

        [Required]
        public int BookingId { get; set; } 
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

    }
}
