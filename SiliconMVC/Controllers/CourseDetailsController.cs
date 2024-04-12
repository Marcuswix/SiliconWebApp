using Infrastructure.Entities;
using Infrastructure.Services;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace SiliconMVC.Controllers
{
    public class CourseDetailsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CourseServices _courseServices;

        public CourseDetailsController(HttpClient httpClient, IConfiguration configuration, CourseServices courseServices)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _courseServices = courseServices;
        }

        private void SetValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = true;
        }

        [HttpGet]
        [Route("/{courseId}")]
        [Authorize(Policy = "AuthenticatedUsers")]
        public async Task <IActionResult> Index(int courseId)
        {
            SetValues();

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiKey = _configuration["ApiKey:Secret"];

                var result = await _courseServices.GetOneCourse(apiKey, courseId);

                if(result != null)
                {
                        return View(result);
                 
                }
            }
            return View();
        }
    }
}
