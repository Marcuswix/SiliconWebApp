using Infrastructure.Models;

namespace Infrastructure.ViewModels
{
    public class AccountAddressDetailsViewModel
    {
        public AccountDetailsAddressModel? AddressInfo { get; set; }

        public string? SuccessMessageAddressInfo { get; set; }

        public string? ErrorMessageAddressInfo { get; set; }
    }
}
