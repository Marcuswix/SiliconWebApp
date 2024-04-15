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


    }
}
