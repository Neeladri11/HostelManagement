using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.View_Models
{
    public class BookingVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int DurationOfStay { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}
