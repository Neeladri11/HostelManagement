using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; } 

        [Required]
        public string Gender { get; set; } 

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; } 

        [Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be in the format xxxxxxxxxx")] 
        public string PhoneNo { get; set; } 

        [Required]
        public string GuardianName { get; set; } 

        [Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be in the format xxxxxxxxxx")] 
        public string GuardianPhno { get; set; } 

        [Required]
        public string MealServices { get; set; }

        [Required]
        public string LaundryServices { get; set; }

        [Required]
        public int RoomId { get; set; } 
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

    }
}
