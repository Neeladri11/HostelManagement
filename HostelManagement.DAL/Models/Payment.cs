using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; } = string.Empty;
        
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string ModeOfPayment { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public string BookingId { get; set; } = string.Empty;
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

    }
}
