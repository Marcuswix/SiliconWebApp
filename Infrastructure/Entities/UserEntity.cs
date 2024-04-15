using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class UserEntity : IdentityUser
    {
        [Required]
        [StringLength(50)]
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        [StringLength(1000)]
        [ProtectedPersonalData]
        public string? Biography { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        [ProtectedPersonalData]
        public string? UrlImage { get; set; }

        public int? AddressId { get; set; }

        [ProtectedPersonalData]
        public AddressEntity? Address { get; set; }

        public bool IsExternalAccount { get; set; } = false;

        public ICollection<UserCourse>? UserCourses { get; set; }
    }
}
