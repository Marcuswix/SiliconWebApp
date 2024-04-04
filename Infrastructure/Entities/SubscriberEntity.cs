using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class SubscriberEntity
    {
        public int Id {  get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "email", Prompt = "Your Email", Order = 0)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Required]
        public bool Newsletter { get; set; }

        [Required]
        public bool EventUpdates { get; set; }

        [Required]
        public bool AdvertisingUpdates { get; set; }

        [Required]
        public bool StartUps { get; set; }

        [Required]
        public bool WeekInReview { get; set; }

        [Required]
        public bool Podcasts { get; set; }
    }
}
