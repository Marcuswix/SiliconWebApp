using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace SiliconMVC.Controllers
{
    public class UpdateCourseController : Controller
    {
        private void SetDefaultViewValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }


        #region [HttpGet] UpdateIndex
        //INDEX
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            SetDefaultViewValues();

            try
            {
                using var http = new HttpClient();

                var response = await http.GetAsync($"https://localhost:7117/api/Courses?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var courses = JsonConvert.DeserializeObject<List<CourseEntity>>(json);
                    var course = courses.FirstOrDefault(x => x.Id == id); // Hämta första kursen från listan

                    return View(course);
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong...";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Something went wrong";
                return View();
            }
        }
        #endregion


        #region [HttpPut] UpdateCourse
        //INDEX
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(CourseEntity course)
        {
            SetDefaultViewValues();

            if (ModelState.IsValid && course != null)
            {

                var model = new CourseModel
                {
                    Title = course.Title,
                    LikesInNumbers = course.LikesInNumbers,
                    LikesInProcent = course.LikesInProcent,
                    Description = course.Description,
                    Author = course.Author,
                    DiscountPrice = course.DiscountPrice,
                    Hours = course.Hours,
                    ImageALtText = course.ImageALtText,
                    ImageUrl = course.ImageUrl,
                    Price = course.Price,
                    WhatYouLearn = course.WhatYouLearn,
                    IsBestseller = course.IsBestseller,
                };

                using var http = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await http.PutAsync($"https://localhost:7117/api/Courses?id={course.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The course was successfully updated!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try agin later.";
                }
            }

            return View("Index");
        }
        #endregion

    }

}