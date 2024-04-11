using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<CourseEntity> GetOneCourse(string apiKey, int id)
        {
            var result = await _httpClient.GetAsync($"https://localhost:7117/api/Courses/{id}?key={apiKey}");

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);
                    var contentResult = jsonObject["contentResult"].ToString();

                    var entity = JsonConvert.DeserializeObject<CourseEntity>(contentResult);

                    if (entity != null)
                    {
                        var viewModel = new CourseEntity
                        {
                            Id = id,
                            Title = entity.Title,
                            Author = entity.Author,
                            Description = entity.Description,
                            DiscountPrice = entity.DiscountPrice,
                            CategoryId = entity.CategoryId,
                            Hours = entity.Hours,
                            ImageALtText = entity.ImageALtText,
                            ImageUrl = entity.ImageUrl,
                            IsBestseller = entity.IsBestseller,
                            LikesInNumbers = entity.LikesInNumbers,
                            LikesInProcent = entity.LikesInProcent,
                            WhatYouLearn = entity.WhatYouLearn,
                            Price = entity.Price,
                        };

                        return viewModel;
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
