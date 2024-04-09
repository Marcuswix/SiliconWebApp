using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PayPal.Api;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class SecurityController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public SecurityController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _userRepository.DeleteOneAsync(x => x.Id == id, id);

                if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch {
                return BadRequest();
            }

        }

    }
}
