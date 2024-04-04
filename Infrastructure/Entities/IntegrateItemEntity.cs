using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class IntegrateItemEntity
    {
        [Key]
        public int Id { get; set; }
        public int IntegrateId { get; set; }
        public IntegrateEntity Integrate { get; set; } = null!;

        [StringLength(50)]
        public string? Headline { get; set; } = null!;
        [StringLength(200)]
        public string? Text { get; set; } = null!;

        [StringLength(500)]
        public string? ImageUrl { get; set; } = null!;

        [StringLength(50)]
        public string? AltText { get; set; } = null!;
    }
}
