using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; } = null!;

        [StringLength(50)]
        public string? StreetName2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(6)]
        public string PostalCode { get; set; } = null!;

        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
