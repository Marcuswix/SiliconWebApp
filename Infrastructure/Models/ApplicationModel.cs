using System.ComponentModel.DataAnnotations;


namespace Infrastructure.Models
{
    public class ApplicationModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a name")]
        [Display(Name = "Name", Prompt = "Enter your name", Order = 0)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "You must enter an emailAddress")]
        [Display(Name = "Email", Prompt = "Enter your Email", Order = 1)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Career", Prompt = "Choose Career", Order = 2)]
        public string? Career { get; set; }

        [Required(ErrorMessage = "You must enter a message")]
        [Display(Name = "Message", Prompt = "Enter a message", Order = 3)]
        public string Message { get; set; } = null!;
    }
}
