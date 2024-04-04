using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class UserModel
    {

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        public string? UrlImage { get; set; }

        public string? Role { get; set; }

        public string? Phone { get; set; }

        [StringLength(1000)]
        public string? Biography { get; set; }

        public int? AddressId { get; set; }

        public AccountDetailsAddressModel? Address { get; set; }
    }
}
