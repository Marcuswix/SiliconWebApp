using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class ContactMessageModel
    {
        [Required(ErrorMessage = "You must enter a name")]
        [Display(Name = "Name", Prompt = "Enter your name", Order = 0)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "You must enter an email address")]
        [Display(Name = "Email", Prompt = "Enter your email", Order = 1)]
        [EmailAddress(ErrorMessage = "You must enter a valid email address")]
        public string Email { get; set; } = null!;

        [Display(Name = "Service", Order = 2)]
        public string? Service { get; set; }

        [Display(Name = "Message", Prompt = "Enter you message here...", Order = 3)]
        [Required(ErrorMessage = "You must enter a message")]
        public string Message { get; set; } = null!;
    }
}
