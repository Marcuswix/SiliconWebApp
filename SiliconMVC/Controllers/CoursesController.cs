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
        public async Task<IActionResult> Index([FromQuery(Name = "category")]int categoryId, [FromQuery(Name = "search")]string search)
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
                            //Where kollar ifall ett viss element innehåller ett visst värde... 
                            var filteredCourses = courses!.Where(course => course.CategoryId == categoryId).ToList();

                            var viewModel = new CourseViewModel
                            {
                                Categories = categories,
                                Courses = filteredCourses
                            };

                            var categoryName = categories.FirstOrDefault(x => x.Id == categoryId);

                            ViewData["Category"] = categoryName!.CategoryName;

                            return View(viewModel);
                        }
                        else if (!string.IsNullOrEmpty(search))
                        {
                            var searchByProductTitle = courses.Where(x => x.Title!.ToLower() != null && x.Title.ToLower().Contains(search.ToLower()));

                            if(searchByProductTitle.Any())
                            {
                                var viewModel = new CourseViewModel
                                {
                                    Courses = searchByProductTitle.ToList(),
                                    Categories = categories
                                };

                                ViewData["Category"] = "Select a Category";
                                return View(viewModel);
                            }

                            var searchByAuthorName = courses.Where(x => x.Author!.ToLower() != null && x.Author.ToLower().Contains(search.ToLower()));

                            if (searchByAuthorName.Any())
                            {
                                var viewModel = new CourseViewModel
                                {
                                    Courses = searchByAuthorName.ToList(),
                                    Categories = categories
                                };
                                ViewData["Category"] = "Select a Category";
                                return View(viewModel);
                            }

                            var searchByCategory = categories.FirstOrDefault(x => x.CategoryName == search);

                            if (searchByCategory != null)
                            {
                                    var filteredCourses = courses!.Where(course => course.CategoryId == searchByCategory.Id).ToList();

                                    var viewModelCategory = new CourseViewModel
                                    {
                                        Courses = filteredCourses,
                                        Categories = categories
                                    };

                                ViewData["Category"] = ViewData["Category"] = "Select a Category"; ;
                                return View(viewModelCategory);
                            }
                        }
                        else
                        {
                            var viewModelAllCourses = new CourseViewModel
                            {
                                Courses = courses,
                                Categories = categories
                            };

                            ViewData["Category"] = "Select a Category";

                            return View(viewModelAllCourses);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "At the moment there are no courses in the database.";
                        return View();
                    }
                }
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
