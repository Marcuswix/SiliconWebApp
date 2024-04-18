using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class ContactController : ControllerBase
    {
        private readonly ContactRepository _contactRepository;

        public ContactController(ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        [Route("message")]
        public async Task<IActionResult> SendMessage(ContactMessageModel model)
        {
            try
            {
                var result = await _contactRepository.SendMessage(model);
                if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok();
                }

                return BadRequest(result.Message);
            }
            catch
            { return BadRequest(); }
        }

        [HttpPost]
        [Route("career")]
        public async Task<IActionResult> SendApplication(ContactCareersModel model)
        {
            try
            {
                var result = await _contactRepository.SendApplication(model);
                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok();
                }

                return BadRequest(result.Message);
            }
            catch
            { return BadRequest(); }
        }
    }
}
