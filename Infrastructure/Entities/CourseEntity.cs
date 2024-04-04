using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class CourseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? ImageALtText { get; set; }
        public string? Price { get; set; }
        public string? DiscountPrice { get; set; }
        public string? Hours { get; set; }
        public bool IsBestseller { get; set; }
        public string? LikesInNumbers { get; set; }
        public string? LikesInProcent { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? WhatYouLearn { get; set;}
        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}
