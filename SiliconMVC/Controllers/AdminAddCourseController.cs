using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using Infrastructure.Helpers;
using Infrastructure.Services;

namespace SiliconMVC.Controllers
{
    public class AdminAddCourseController : Controller
    {
        private readonly GetTokenAndApiKey _getTokenAndApiKey;
        private readonly AdminCoursesServices _adminCoursesServices;

        public AdminAddCourseController(GetTokenAndApiKey getTokenAndApiKey, AdminCoursesServices adminCoursesServices)
        {
            _getTokenAndApiKey = getTokenAndApiKey;
            _adminCoursesServices = adminCoursesServices;
        }

        private void SetValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        [HttpGet]
        [Route("/admin/create-course")]
        public async Task <IActionResult> Index()
        {
            var model = new CourseModel();
            SetValues();
            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> Create(CourseModel model)
        {
            SetValues();

            if (ModelState.IsValid) 
            {
                var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

                var response = await _adminCoursesServices.CreateCourse(token, apiKey, model);

                if(response.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    TempData["SuccessMessage"] = "Course was added successfully";
                    return View("Index");
                }
                if(response.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
                {
                    TempData["ErrorMessage"] = "A course with same title already exist.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try agin later.";
                }
            }
            return View("Index");

        }
    }
}
