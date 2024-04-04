using Infrastructure.Helpers;
using SiliconMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class SubscribeModel
    {
        [Required(ErrorMessage = "A valid email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email", Prompt = "Your Email", Order = 0)]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Newsletter", Order = 1)]
        public bool Newsletter { get; set; }

        [Display(Name = "EventUpdates", Order = 2)]
        public bool EventUpdates { get; set; }

        [Display(Name = "AdvertisingUpdates", Order = 3)]
        public bool AdvertisingUpdates { get; set; }

        [Display(Name = "StartUps", Order = 4)]
        public bool StartUps { get; set; }

        [Display(Name = "WeekInReview ", Order = 5)]
        public bool WeekInReview { get; set; }

        [Display(Name = "Podcasts", Order = 6)]
        public bool Podcasts { get; set; }

        [AtLeastOneOptionIsSelectedSubscribe(ErrorMessage = "You must select at least one option.")]
        [Display(Name = "AtLeastOneOptionSelected")]
        public bool AtLeastOneOptionSelected => (Newsletter) || (EventUpdates) || (AdvertisingUpdates) || (StartUps) || (WeekInReview) || (Podcasts);

        public string? ErrorMessage { get; set; }

    }
}
