using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;

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

        public async Task<List<SubscriberEntity>> GetAllSubscribers(string token, string apiKey)
        {
            try
            {
                if (token != null && apiKey != null)
                {
                    using var http = new HttpClient();

                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await http.GetAsync($"https://localhost:7117/api/Subscribe?key={apiKey}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(json);

                        if (apiResponse!.StatusCode == 200)
                        {
                            var listSubscribers = apiResponse.ContentResult;

                            return listSubscribers;
                        }
                    }
                    else
                    {
                        return null!;
                    }
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }


        public async Task<RepositoriesResult> Delete(string email, string token, string apiKey)
        {

            if (email != null && token != null && apiKey != null)
            {
                using var http = new HttpClient();

                var response = await http.DeleteAsync($"https://localhost:7117/api/Subscribe?email={email}&token={token}&key={apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    return ResponseFactory.Ok();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return ResponseFactory.NotFound();
                }
            }
            return ResponseFactory.Error();
        }
    }
}
