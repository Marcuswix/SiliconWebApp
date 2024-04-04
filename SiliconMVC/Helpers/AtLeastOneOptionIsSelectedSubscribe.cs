using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Helpers
{
    public class AtLeastOneOptionIsSelectedSubscribe : ValidationAttribute
    {
        public class AtLeastOneOptionIsSelectedAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var subscribeModel = (SubscribeModel)validationContext.ObjectInstance;
                if (subscribeModel.Newsletter == true || subscribeModel.EventUpdates == true || subscribeModel.AdvertisingUpdates == true || subscribeModel.StartUps == true || subscribeModel.WeekInReview == true || subscribeModel.Podcasts == true)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
