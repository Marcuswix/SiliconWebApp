using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public UserServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<RepositoriesResult> DeleteUser(string id, string apiKey)
        {
            try
            {
                if(id != null)
                {
                    var result = await _httpClient.DeleteAsync($"https://localhost:7117/api/Security?id={id}&key={apiKey}");
                    
                    if(result.IsSuccessStatusCode)
                    {
                        return ResponseFactory.Ok();
                    }

                    return ResponseFactory.Error();
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error();
            }
        }

        public async Task<RepositoriesResult> AddUserCourse(string apiKey, string userId, int courseId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7117/api/Auth/userCourse?courseId={courseId}&userId={userId}&key={apiKey}", null);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var entity = JsonConvert.DeserializeObject<UserEntity>(json);
                    return ResponseFactory.Ok();
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
