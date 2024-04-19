using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [UseApiKey]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly SubscribeRepository _subscribeRepository;

        public SubscribeController(SubscribeRepository subscribeRepository)
        {
            _subscribeRepository = subscribeRepository;
        }

        #region [HttpPost] Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscribeModel model)
        {
            if (!string.IsNullOrEmpty(model.Email) && ModelState.IsValid)
            {
                    var result = await _subscribeRepository.AddSubscriber(model);

                    if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                    {
                        return Created("", null);
                    }
                    if(result.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
                    {
                        return Conflict("The email address already is up for subscription");
                    }
                    if(result.StatusCode == Infrastructure.Models.StatusCodes.ERROR)
                    {
                        return Problem("Unable to create subscription");
                    }
            }

            return BadRequest();
        }
        #endregion


        #region [HttpGet] Get/Read
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
                var subscribers = await _subscribeRepository.GetAll();

                if(subscribers.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok(subscribers);
                }
                else if(subscribers.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    return NotFound();
                }
                else
                return BadRequest(); 
        }
        #endregion

        #region [HttpGet] GetOne
        [HttpGet("/api/Subscribe/getone/{email}")]
        public async Task<IActionResult> GetOne(string email)
        {
                var result = await _subscribeRepository.GetOne(email);

                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok(result);
                }

            return NotFound();
        }
        #endregion

        #region [HttpPut] Update
        [HttpPut]
        public async Task <IActionResult> Update(int id, SubscribeModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var result = await _subscribeRepository.Update(id, model);

                if(result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok(result);
                }
                if(result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }
        #endregion

        #region [HttpDelete] Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            if (email != null)
            {
                var subscribeEmail = new SubscribeModel { Email = email };

                var result = await _subscribeRepository.Delete(subscribeEmail);

                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    return Ok();
                }
                if (result.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion
    }
}