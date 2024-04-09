using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Infrastructure.Models
{
    public class PasswordModel
    {
        [Required(ErrorMessage = "You must enter your current password")]
        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "NewPassword", Prompt = "Enter a new password")]
        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z]).{8,}$", ErrorMessage = "Invalid password")]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "ConfirmNewPassword", Prompt = "Confirm your new password")]
        [Required(ErrorMessage = "Please confirm your new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The passwords do not match")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
