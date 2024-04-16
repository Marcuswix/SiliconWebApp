using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;

namespace SiliconMVC.Controllers
{
    public class MyCoursesController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly CourseServices _courseServices;
        private readonly MyCoursesServices _myCoursesServices;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MyCoursesController(UserManager<UserEntity> userManager, CourseServices courseServices, MyCoursesServices myCoursesServices, HttpClient httpClient, IConfiguration configuration)
        {
            _userManager = userManager;
            _courseServices = courseServices;
            _myCoursesServices = myCoursesServices;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private void setValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = true;
        }

        [HttpGet]
        [Route("/mycourses")]
        public async Task<IActionResult> Index()
        {
            setValues();

            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var apiKey = _configuration["ApiKey:Secret"];

                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var result = await _myCoursesServices.GetAll(user.Id, apiKey);

                        if (result.Count() != 0)
                        {
                            return View(result);
                        }
                    }
                }  
                return View();
            }
            catch (Exception ex) {Debug.WriteLine(ex.Message); return View(); 
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteOne(int courseId)
        {
            setValues();

            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var apiKey = _configuration["ApiKey:Secret"];

                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var result = await _myCoursesServices.DeleteACourse(apiKey, user.Id, courseId);

                        if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                        {
                            TempData["SuccessMessage"] = "The course was deleted successfully!";
                            return View(result);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Something went wrong...";
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); return View();
            }
        }

    }
}
