using Infrastructure.Entities;
using Infrastructure.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.Services
{
    public class UserCourseServices
    {

        private readonly HttpClient _httpClient;
        private readonly CourseResponse _response;

        public UserCourseServices(HttpClient httpClient, CourseResponse response)
        {
            _httpClient = httpClient;
            _response = response;
        }

        public async Task<UserCourse> AddUserCourse(string apiKey, string userId, int courseId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7117/api/Auth/userCourse?courseId={courseId}&userId={userId}&key={apiKey}", null);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var entity = JsonConvert.DeserializeObject<UserCourse>(json);
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
