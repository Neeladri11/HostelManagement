using System.ComponentModel.DataAnnotations;

namespace HostelManagement.DAL.View_Models
{
    public class MealVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string MealType { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}
