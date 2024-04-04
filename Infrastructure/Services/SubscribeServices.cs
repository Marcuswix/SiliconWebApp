using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.Services
{
    public class SubscribeServices
    {
        private readonly SubscribeRepository _repository;

        public SubscribeServices(SubscribeRepository repository)
        {
            _repository = repository;
        }

        public async Task<RepositoriesResult> AddSubscriber(SubscribeModel model)
        {
            if (model != null) 
            {
                var result = await _repository.AddSubscriber(model);

                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return ResponseFactory.Ok("Your subscribtion is added");
                }
                if (result.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
                {
                    return ResponseFactory.AlreadyExist("This email is already up for subscription");
                }

                return ResponseFactory.NotFound();
            }

            return ResponseFactory.Error("Could add a subscribtion");
        }
    }
}
