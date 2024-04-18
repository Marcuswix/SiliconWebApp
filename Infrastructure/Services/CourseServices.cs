using Infrastructure.Entities;
using Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Infrastructure.Services
{
    public class CourseServices
    {
        private readonly HttpClient _httpClient;
        private readonly CourseResponse _response;

        public CourseServices(HttpClient httpClient, CourseResponse response)
        {
            _httpClient = httpClient;
            _response = response;
        }

        public async Task<List<CourseEntity>> GetCourses(string apiKey)
        {
            try
            {
                var coursesResponse = await _httpClient.GetAsync($"https://localhost:7117/api/Courses?key={apiKey}");

                if (coursesResponse.IsSuccessStatusCode)
                {
                    var coursesJson = await coursesResponse.Content.ReadAsStringAsync();
                    var coursesObject = JObject.Parse(coursesJson);
                    var coursesArray = coursesObject["contentResult"]["$values"].ToObject<List<CourseEntity>>();

                    var categoriesResponse = await _httpClient.GetAsync("https://localhost:7117/api/Category");
                    var categoriesJson = await categoriesResponse.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(categoriesJson);

                    foreach (var course in coursesArray)
                    {
                        course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
                    }

                    return coursesArray;
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

        public async Task<List<CourseEntity>> GetCoursesBySearch(string apiKey, string search)
        {

            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7117/api/Courses/GetBySearch?search={search}?key={apiKey}");

                var categories = await _httpClient.GetAsync("https://localhost:7117/api/Category");

                var jsonResult = await categories.Content.ReadAsStringAsync();
                var categoriesList = JsonConvert.DeserializeObject<List<CategoryEntity>>(jsonResult);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var allCourses = JsonConvert.DeserializeObject<List<CourseEntity>>(json);

                    return allCourses;
                }
                else
                { return null!; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!; ;
            }
        }

        public async Task<CourseEntity> GetOneCourse(string apiKey, int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7117/api/Courses/{id}?key={apiKey}");

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    var courseJson = await result.Content.ReadAsStringAsync();
                    var courseObject = JsonConvert.DeserializeObject<CourseResponse>(courseJson);

                    if (courseObject?.ContentResult != null)
                    {
                        return courseObject.ContentResult;
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
            else
            {
                return null;
            }
        }

        public async Task<TeacherEntity> GetOneTeacher(string apiKey, int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7117/api/Teacher/{id}");

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);
                    var contentResult = jsonObject["contentResult"].ToString();
                    var entity = JsonConvert.DeserializeObject<TeacherEntity>(contentResult);

                    if (entity != null)
                    {
                        return entity;
                    }

                    return null!;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null!;
                }
            }

            return null!;

        }
    }
}
