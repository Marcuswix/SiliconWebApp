using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class PasswordModel
    {
        [Required(ErrorMessage = "Incorrect password")]
        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "NewPassword", Prompt = "Enter a new password")]
        [Required(ErrorMessage = "Not a vaild password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z]).{8,}$", ErrorMessage = "Invalid password")]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "ConfirmNewPassword", Prompt = "Confirm your new password")]
        [Required(ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
