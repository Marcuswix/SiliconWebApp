using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace SiliconMVC.Controllers
{
    [Authorize(Policy = "Admins")]
    public class AdminCoursesController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminCoursesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void SetDefaultViewValues()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
        }

        //Det ska gå att utföra CRUD på alla delar i Courses!!

        [HttpGet]
        [Route("/admin/courses")]
        public async Task<IActionResult> Index()
        {
            SetDefaultViewValues();

            var response = await _httpClient.GetAsync("https://localhost:7117/api/Courses?key=920344b7-dd86-4721-9ce0-92e80f7d7da4");

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<CourseEntity>>(json);

            return View(data);
        }

        [HttpPost]

        public async Task<IActionResult> CreateCourse(CourseModel model)
        {
            SetDefaultViewValues();

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
        public async Task<IActionResult> Delete(int id)
        {
            SetDefaultViewValues();

            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7117/api/Courses?id={id}");

                if(response.IsSuccessStatusCode)
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
