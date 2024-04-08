using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Infrastructure.Services
{
    public class CourseServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CourseServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<CourseEntity>> GetCourses(string apiKey)
        {

            try
            {
                    var response = await _httpClient.GetAsync($"https://localhost:7117/api/Courses?key={apiKey}");

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
                return null!;;
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
    }
}
