using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class EmailUsModel
    {
        [Required]
        [Display(Name = "Name", Prompt = "Enter your name", Order = 0)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Enter your email", Order = 1)]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
        public string Service { get; set; } = null!;
        [Required]
        [Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
        public string Message { get; set; } = null!;
    }
}
