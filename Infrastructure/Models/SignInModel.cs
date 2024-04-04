using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class SignInModel
    {
            [Display(Name = "Email", Prompt = "Enter your email", Order = 0)]
            [EmailAddress]
            [Required(ErrorMessage = "An email is required")]
            public string Email { get; set; } = null!;

            [Display(Name = "Password", Prompt = "Enter your password", Order = 1)]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "A password is required")]
            public string Password { get; set; } = null!;

            [Display(Name = "Remeber me", Order = 3)]
            public bool RememberMe { get; set; }
    }
}
