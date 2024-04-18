using Infrastructure.Models;

namespace Infrastructure.ViewModels
{
    public class MessageViewModel
    {
        public ContactMessageModel? Message { get; set; }

        public ContactCareersModel? Application { get; set; }
    }
}
