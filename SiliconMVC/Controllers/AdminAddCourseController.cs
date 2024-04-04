using Azure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;

namespace SiliconMVC.Controllers
{
    public class AdminAddCourseController : Controller
    {
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
                var http = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await http.PostAsync("https://localhost:7117/api/Courses", content);

                if(response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Course was added successfully";
                    var newModel = new CourseModel();
                    return View("Index", newModel);
                }
                if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["ErrorMessage"] = "A course with same title already exist.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try agin later.";
                }
            }
            return View("Index", model);

        }
    }
}
