using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class CourseModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(60)]
        public string Title { get; set; } = null!;

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Alt")]
        public string? ImageALtText { get; set; }

        [Display(Name = "Price")]
        public string? Price { get; set; }

        [Display(Name = "DiscountPrice")]
        public string? DiscountPrice { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category", Prompt = "Select a category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Hours")]
        public string? Hours { get; set; }

        [Display(Name = "IsBestseller")]
        public bool IsBestseller { get; set; }

        [Display(Name = "LikesInNumbers")]
        public string? LikesInNumbers { get; set; }

        [Display(Name = "LikesInProcent")]
        public string? LikesInProcent { get; set; }

        [Display(Name = "Author")]
        [StringLength(50)]
        public string? Author { get; set; }

        [StringLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "WhatYouLearn")]
        public string? WhatYouLearn { get; set; }

        public string ErrorrMessage { get; set; } = string.Empty;
    }
}
