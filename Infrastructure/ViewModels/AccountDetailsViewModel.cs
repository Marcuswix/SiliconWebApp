using Infrastructure.Entities;

namespace Infrastructure.ViewModels
{
    public class AccountDetailsViewModel
    {
        public UserEntity? User { get; set; }

        public bool IsExternalAccount { get; set; }
    }
}
