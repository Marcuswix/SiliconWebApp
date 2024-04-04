using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "You must select a category")]
        [StringLength(100)]
        [Display(Name = "Category", Prompt = "Select a category")]
        public string CategoryName { get; set; } = null!;
    }
}
