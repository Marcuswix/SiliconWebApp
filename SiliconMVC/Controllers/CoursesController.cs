using Azure;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Infrastructure.Controllers
{
    public class CoursesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;

        public CoursesController(HttpClient httpClient, IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userManager = userManager;
        }

        private void setValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        [HttpGet]
        [Route("/courses")]
        [Authorize(Policy = "AuthenticatedUsers")]
        public async Task<IActionResult> Index(int categoryId, string search)
        {
            setValues();

            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var apiKey = _configuration["ApiKey:Secret"];

                    var response = await _httpClient.GetAsync($"https://localhost:7117/api/Courses?key={apiKey}");

                    var categories = await _httpClient.GetAsync("https://localhost:7117/api/Category");

                    var jsonResult = await categories.Content.ReadAsStringAsync();
                    var categoriesList = JsonConvert.DeserializeObject<List<CategoryEntity>>(jsonResult);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var allCourses = JsonConvert.DeserializeObject<List<CourseEntity>>(json);

                        //Where kollar ifall ett viss element innehåller ett visst värde... 
                        var filteredCourses = allCourses.Where(course => course.CategoryId == categoryId).ToList();

                        var viewModel = new CourseViewModel
                        {
                            Categories = categoriesList,
                            Courses = filteredCourses
                        };

                        return View(viewModel);
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "At the moment there are no courses in the database.";
                        return View();
                    }
                }
                else
                { return View(); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Something went wrong";
                return View();
            }
        }
    }
}
