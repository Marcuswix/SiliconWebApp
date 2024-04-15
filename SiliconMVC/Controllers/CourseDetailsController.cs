using Infrastructure.Services;
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
        private readonly UserServices _userServices;


        public CourseDetailsController(HttpClient httpClient, IConfiguration configuration, CourseServices courseServices, UserServices userServices)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _courseServices = courseServices;
            _userServices = userServices;
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

        [HttpPost]
        [Route("/{courseId}")]
        [Authorize(Policy = "AuthenticatedUsers")]
        public async Task<IActionResult> JoinCourse(int courseId, string userId)
        {
            SetValues();

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiKey = _configuration["ApiKey:Secret"];

                var result = await _userServices.AddUserCourse(apiKey, userId, courseId);
//´LäGGTILL MYCOURES
                if (result != null)
                {
                    return RedirectToAction("...");

                }
            }
            return View();
        }
    }
}
