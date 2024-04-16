using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class MyCoursesController : ControllerBase
    {
        private readonly MyCoursesReporsitory _myCoursesReporsitory;

        public MyCoursesController(MyCoursesReporsitory myCoursesReporsitory)
        {
            _myCoursesReporsitory = myCoursesReporsitory;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(string userId)
        {
            try
            {
                if(userId != null)
                {
                    var result = await _myCoursesReporsitory.GetCourses(userId);
                    
                    if(result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(int courseId, string userId)
        {
            try
            {
                var alreadyHaveCourseAdded = await _myCoursesReporsitory.CourseExists(userId, courseId);

                if(alreadyHaveCourseAdded == false)
                {
                    var result = await _myCoursesReporsitory.AddCourse(courseId, userId);
                    
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                if(alreadyHaveCourseAdded == true)
                {
                    return Conflict();
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/delete")]
        public async Task<IActionResult> DeleteCourse(int courseId, string userId)
        {
            var result = await _myCoursesReporsitory.DeleteACourse(courseId, userId);

            if (result == true)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("/deleteAllCourses")]
        public async Task<IActionResult> DeleteAllCourse(int courseId, string userId)
        {
            var result = await _myCoursesReporsitory.DeleteAllCourses(userId);

            if (result == true)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
