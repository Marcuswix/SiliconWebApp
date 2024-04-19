using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Services;
using Infrastructure.ViewModels;
using System.Net.Http.Headers;
using Infrastructure.Helpers;

namespace SiliconMVC.Controllers
{
    [Authorize(Policy = "Admins")]
    public class AdminCoursesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CourseServices _courseServices;
        private readonly CategoryServices _categoryServices;
        private readonly GetTokenAndApiKey _getTokenAndApiKey;

        public AdminCoursesController(HttpClient httpClient, IConfiguration configuration, CourseServices courseServices, CategoryServices categoryServices, GetTokenAndApiKey getTokenAndApiKey)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _courseServices = courseServices;
            _categoryServices = categoryServices;
            _getTokenAndApiKey = getTokenAndApiKey;
        }

        private void SetValues()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
        }

        //Det ska gå att utföra CRUD på alla delar i Courses!!

        [HttpGet]
        [Route("/admin/courses")]
        public async Task<IActionResult> Index([FromQuery(Name = "category")] int categoryId, [FromQuery(Name = "search")] string search, int pageNumber = 1, int pageSize = 9)
        {
            SetValues();

            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var apiKey = _configuration["ApiKey:Secret"];

                    var courses = await _courseServices.GetCourses(apiKey!);

                    var categories = await _categoryServices.getAllCategories();

                    if (courses != null)
                    {
                        if (categoryId != 0)
                        {
                            var filteredCourses = courses!.Where(course => course.CategoryId == categoryId).ToList();

                            if (filteredCourses.Count > 0)
                            {
                                var viewModelbyCategory = new CourseViewModel
                                {
                                    Categories = categories,
                                    Courses = filteredCourses,
                                    CurrentPage = pageNumber,
                                    TotalPages = (int)Math.Ceiling((decimal)filteredCourses.Count / pageSize)
                                };

                                var categoryName = categories.FirstOrDefault(x => x.Id == categoryId);

                                ViewData["Category"] = categoryName!.CategoryName;

                                return View(viewModelbyCategory);
                            }

                        }
                        else if (!string.IsNullOrEmpty(search))
                        {
                            courses = courses.Where(x =>
                               x.Title != null && x.Title.ToLower().Contains(search.ToLower()) ||
                               x.Author != null && x.Author.ToLower().Contains(search.ToLower())
                           ).ToList();

                            var searchByCategory = categories.Where(x => x.CategoryName.ToLower().Contains(search.ToLower())
                           ).ToList();

                            if (searchByCategory.Any())
                            {
                                var categoryFound = searchByCategory.First();

                                var filteredCourses = courses!.Where(course => course.CategoryId == categoryFound.Id).ToList();

                                var viewModelCategory = new CourseViewModel
                                {
                                    Courses = filteredCourses,
                                    Categories = categories,
                                    CurrentPage = pageNumber,
                                    TotalPages = (int)Math.Ceiling((decimal)filteredCourses.Count / pageSize)
                                };

                                ViewData["Category"] = categoryFound.CategoryName;
                                return View(viewModelCategory);
                            }
                            else
                            {
                                var viewModelAllCourses = new CourseViewModel
                                {
                                    Categories = categories,
                                    Courses = courses,
                                    CurrentPage = pageNumber,
                                    TotalPages = (int)Math.Ceiling((decimal)courses!.Count / pageSize)
                                };

                                TempData["ErrorMessage"] = "No courses found...";
                                ViewData["Category"] = "Select a Category"; ;
                                return View(viewModelAllCourses);
                            }
                        }

                        var paginatedCourse = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                        var viewModel = new CourseViewModel
                        {
                            Categories = categories,
                            Courses = paginatedCourse,
                            CurrentPage = pageNumber,
                            TotalPages = (int)Math.Ceiling((decimal)courses.Count / pageSize)
                        };

                        ViewData["Category"] = "Select a Category";
                        return View(viewModel);
                    }
                    else
                    {
                        ViewData["Category"] = "Select a Category";
                        var viewModelAllCourses = new CourseViewModel
                        {
                            Categories = categories,
                            Courses = courses,
                            CurrentPage = pageNumber,
                            TotalPages = (int)Math.Ceiling((decimal)courses!.Count / pageSize)
                        };
                        return View(viewModelAllCourses);
                    }
                }
                TempData["ErrorMessage"] = "No courses found...";
                return View();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Something went wrong";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseModel model)
        {
            SetValues();

            try
            {
                if (ModelState.IsValid)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("https://localhost:7117/api/Courses", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "The course was successfully added!";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        TempData["ErrorMessage"] = "You haven't filled out the forms correct..";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Someting went wrong";
                    }

                    return View(model);
                }
            }
            catch (Exception ex) { Debug.WriteLine("CreateCourse" + ex.Message); }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int courseId)
        {
            SetValues();
            try
            {
                var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

                var result = await _courseServices.DeleteCourse(apiKey, courseId);

                if (result == true)
                {
                    TempData["SuccessMessage"] = "The course was successfully deleted";
                }
                else
                {
                    TempData["ErrorMessage"] = "Someting went wrong";
                }
            }
            catch (Exception ex) { Debug.WriteLine("DeleteCourse" + ex.Message); }

            return RedirectToAction("Index");
        }
    }
}
