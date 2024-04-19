using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Services
{
    public class SubscribeServices
    {
        public async Task<RepositoriesResult> AddSubscriber(SubscribeModel model, string apiKey)
        {
            if (model != null && apiKey != null) 
            {
                using var http = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await http.PostAsync($"https://localhost:7117/api/Subscribe?key={apiKey}", content);

                if (result.IsSuccessStatusCode)
                {
                    return ResponseFactory.Ok();
                }
                if (result.StatusCode == HttpStatusCode.Conflict)
                {
                    return ResponseFactory.AlreadyExist();
                }

                return ResponseFactory.NotFound();
            }

            return ResponseFactory.Error();
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

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<RepositoriesResult> GetOne(string email, string token, string apiKey)
        {

            if (email != null && token != null && apiKey != null)
            {
                using var http = new HttpClient();

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await http.GetAsync($"https://localhost:7117/api/Subscribe/getone/{email}?key={apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<ApiResponseModelOneSubscriber>(json);

                    if (result?.ContentResult != null)
                    {
                        return ResponseFactory.Ok(result.ContentResult);
                    }
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
