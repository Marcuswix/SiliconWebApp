using Infrastructure.Models;

namespace Infrastructure.ViewModels
{
    public class SignUpViewModel
    {
        public string Title { get; set; } = "Sign up";

        public string? ErrorMessage { get; set; }
        
        public SignUpModel Form { get; set; } = new SignUpModel();
    }
}
