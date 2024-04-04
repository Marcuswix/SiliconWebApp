using Infrastructure.Models;

namespace Infrastructure.ViewModels
{
    public class SignInViewModel
    {
        public SignInModel Form { get; set; } = new SignInModel();
        public string? ErrorMessage { get; set; }
    }
}
