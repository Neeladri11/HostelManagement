using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be in the format xxxxxxxxxx")] 
        public string PhoneNo { get; set; } = string.Empty;

        [Required]
        public string GuardianName { get; set; } = string.Empty;

        [Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be in the format xxxxxxxxxx")] 
        public string GuardianPhno { get; set; } = string.Empty;

        [Required]
        public bool MealServices { get; set; }

        [Required]
        public bool LaundryServices { get; set; }

        [Required]
        public string RoomId { get; set; } = string.Empty;
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

    }
}
