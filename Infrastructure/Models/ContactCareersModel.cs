using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class ContactCareersModel
    {
        [Required(ErrorMessage = "You must enter a name")]
        [Display(Name = "Name", Prompt = "Enter your name", Order = 0)]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "You must enter an email address")]
        [Display(Name = "Email", Prompt = "Enter your email", Order = 1)]
        [EmailAddress(ErrorMessage = "You must enter a valid email address")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Display(Name = "Service", Order = 2)]
        public string? Career { get; set; }

        [Display(Name = "Message", Prompt = "Enter you message here...", Order = 3)]
        [Required(ErrorMessage = "You must enter a message")]
        [StringLength(1000, ErrorMessage = "You message can only contain 1000 signs...")]
        public string Message { get; set; } = null!;
    }
}
