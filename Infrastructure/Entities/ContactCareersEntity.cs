using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ContactCareersEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        public string? Career { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = null!;

        public DateTime Time { get; set; }

        public ContactCareersEntity() 
        {
            Time = DateTime.Now;
        }
    }
}
