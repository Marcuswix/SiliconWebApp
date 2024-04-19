using Infrastructure.Entities;

namespace Infrastructure.ViewModels
{
    public class SubscriberViewModel
    {
        public SubscriberEntity? SubscriberEntity { get; set; }

        public List<SubscriberEntity>? Subscribers { get; set; }
    }
}
