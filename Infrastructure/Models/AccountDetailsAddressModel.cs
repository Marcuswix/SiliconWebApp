using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AccountDetailsAddressModel
    {
        [Display(Name = "Address", Prompt = "Enter your address line", Order = 0)]
        [Required(ErrorMessage = "An Address is required")]
        public string Address { get; set; } = null!;

        [Display(Name = "Address2", Prompt = "Enter your second address line", Order = 1)]
        public string? Address2 { get; set; }

        [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
        [Required(ErrorMessage = "A postal code i required")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
        [Required(ErrorMessage = "A city is required")]
        public string City { get; set; } = null!;
    }
}
