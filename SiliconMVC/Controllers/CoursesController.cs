using Azure;
using Azure.Core;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
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
        private readonly CourseServices _courseServices;
        private readonly CategoryServices _categoryServices;

        public CoursesController(HttpClient httpClient, IConfiguration configuration, UserManager<UserEntity> userManager, CourseServices courseServices, CategoryServices categoryServices)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userManager = userManager;
            _courseServices = courseServices;
            _categoryServices = categoryServices;
        }

        private void setValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        [HttpGet]
        [Route("/courses")]
        [Authorize(Policy = "AuthenticatedUsers")]
        public async Task<IActionResult> Index([FromQuery(Name = "category")]int categoryId, [FromQuery(Name = "search")]string search, int pageNumber = 1, int pageSize = 9)
        {
            setValues();

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

                            if(filteredCourses.Count > 0)
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
                            TotalPages = (int)Math.Ceiling((decimal)courses.Count / pageSize)
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
    }
}
