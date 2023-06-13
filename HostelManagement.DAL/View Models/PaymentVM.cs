using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.View_Models
{
    public class PaymentVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string ModeOfPayment { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int BookingId { get; set; }
    }
}
