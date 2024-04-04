using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.Models
{
    public class AccountDetailsModel
    {
        [Key]
        public string? Id { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? ProfileImage { get; set; }

        [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
        [Required(ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Invalid last name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
        [EmailAddress]
        [Required(ErrorMessage = "An email is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone (Optional)", Prompt = "Enter your phone number", Order = 3)]
        [Required(ErrorMessage = "Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
        [DataType(DataType.MultilineText)]
        public string? Biography { get; set; }
    }
}

