﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class MyCoursesServices
    {
        private readonly HttpClient _httpClient;

        public MyCoursesServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseEntity>> GetAll(string userId, string apiKey)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7117/api/MyCourses?userId={userId}&key={apiKey}");

                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var allCourses = JsonConvert.DeserializeObject<List<CourseEntity>>(json);

                    return allCourses;
                }

                return null!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<RepositoriesResult> AddUserCourse(string apiKey, string userId, int courseId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7117/api/MyCourses?courseId={courseId}&userId={userId}&key={apiKey}", null);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var entity = JsonConvert.DeserializeObject<UserEntity>(json);
                    return ResponseFactory.Ok();
                }
                if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return ResponseFactory.AlreadyExist();
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<RepositoriesResult> DeleteACourse(string apiKey, string userId, int courseId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7117/delete?courseId={courseId}&userId={userId}&key={apiKey}", null);

                if (response.IsSuccessStatusCode)
                {
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