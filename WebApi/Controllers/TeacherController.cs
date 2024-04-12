using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherRepository _repository;

        public TeacherController(TeacherRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> getOneTeacher(int id)
        {
            try
            {
                var result = await _repository.GetOneTeacher(id);

                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return null!; 
            };
        }
    }
}
