using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly MyCoursesServices _myCoursesServices;
        private readonly UserManager<UserEntity> _userManager;


        public CourseDetailsController(HttpClient httpClient, IConfiguration configuration, CourseServices courseServices, MyCoursesServices myCoursesServices, UserManager<UserEntity> userManager)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _courseServices = courseServices;
            _myCoursesServices = myCoursesServices;
            _userManager = userManager;
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
        public async Task<IActionResult> JoinCourse(int courseId)
        {
            SetValues();

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiKey = _configuration["ApiKey:Secret"];

                var user = await _userManager.GetUserAsync(User);

                var result = await _myCoursesServices.AddUserCourse(apiKey, user.Id, courseId);

                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return RedirectToAction("Index", "MyCourses");
                }
                if(result.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
                {
                    TempData["ErrorMessage"] = "This course is already added to your course library";
                    return RedirectToAction("Index", "Courses");
                }
            }
            return View();
        }
    }
}
