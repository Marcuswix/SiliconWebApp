using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;
using System.Net.Http.Headers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    //[Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesRepository _courseRepository;
        private readonly DataContext _dataContext;

        public CoursesController(CoursesRepository courseRepository, DataContext dataContext)
        {
            _courseRepository = courseRepository;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _dataContext.Courses.ToListAsync();

            if(result != null)
            {
                return Ok(result);
            }
            if (result == null)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var result = _courseRepository.GetOne(id);


            if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
            {
                return Ok(result);
            }
            if (result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
            {
                return NoContent();
            }
            if (result.StatusCode == Infrastructure.Models.StatusCodes.ERROR)
            {
                return BadRequest();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseModel model)
        {
            if(ModelState.IsValid && model != null)
            {
                var unName = _courseRepository.NameExist(model.Title);

                if(unName.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    var result = await _courseRepository.CreateOne(model);

                    if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                    {
                        return Created("", result);
                    }
                    if (result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                    {
                        return NoContent();
                    }
                    if (result.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
                    {
                        return Conflict("A course with the same title already exists");
                    }
                }
                if(unName.StatusCode == Infrastructure.Models.StatusCodes.EXISTS) 
                {
                    return Conflict("A course with the same title already exists");
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseRepository.Delete(id);

            if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
            {
                return Ok();
            }
            if(result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CourseModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _courseRepository.Update(id, model);

                if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok();
                }
                if(result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }
    }
}
