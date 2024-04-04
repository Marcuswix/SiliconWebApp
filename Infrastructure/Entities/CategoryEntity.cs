using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Category", Prompt = "Enter a category")]
        public string CategoryName { get; set; } = null!;

        public List<CourseEntity> Courses { get; set; }
    }
}
