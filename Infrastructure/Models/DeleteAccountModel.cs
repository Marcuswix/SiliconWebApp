using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class DeleteAccountModel
    {
        [Required]
        [Display(Name = "Delete my account", Order = 0)]
        [RequiredCheckbox(ErrorMessage = "You must confirm the terms & conditions")]
        public bool TermsAndConditions { get; set; }
    }
}
