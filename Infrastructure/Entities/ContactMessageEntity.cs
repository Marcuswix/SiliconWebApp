using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Infrastructure.Entities
{
    public class ContactMessageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        public DateTime Time { get; set; }

        public string? Service { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = null!;

        public ContactMessageEntity() 
        {
            Time = DateTime.Now;
        }
    }
}
