using Infrastructure.Entities;

namespace Infrastructure.Models
{
    public class ApiResponseModelOneSubscriber
    {
        public SubscriberEntity ContentResult { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
