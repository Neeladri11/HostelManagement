using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagement.DAL.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MealType { get; set; }

        //Foreign Key
        [Required]
        public int StudentId { get; set; }
        
        //Navigation property
        public virtual Student students { get; set; }
    }
}
