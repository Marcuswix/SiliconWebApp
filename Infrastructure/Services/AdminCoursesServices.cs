using Azure;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Services
{
    public class AdminCoursesServices
    {
        private readonly GetTokenAndApiKey _getTokenAndApiKey;

        public AdminCoursesServices(GetTokenAndApiKey getTokenAndApiKey)
        {
            _getTokenAndApiKey = getTokenAndApiKey;
        }

        public async Task<RepositoriesResult> CreateCourse(string token, string apiKey, CourseModel model)
        {
            try
            {
                
                var http = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await http.PostAsync($"https://localhost:7117/api/Courses?token={token}&apiKey={apiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    return ResponseFactory.Ok();
                }

                return ResponseFactory.Error();
            }
            catch {
                return ResponseFactory.Error(); ;
            }

        }
    }
}
