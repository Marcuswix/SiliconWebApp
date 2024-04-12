using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDetailsController : ControllerBase
    {
        private readonly CourseDetailsRepository _courseDetailsRepository;

        public CourseDetailsController(CourseDetailsRepository courseDetailsRepository)
        {
            _courseDetailsRepository = courseDetailsRepository;
        }
        //RADERA???!!!!!
        //[HttpGet]
        //[Route("/whatYouLearn")]
        //public async Task<WhatYouLearnEntity> GetWhatYouLearn(int id)
        //{
        //    var result = await _courseDetailsRepository.GetWhatYouLearn(id);

        //    if(result != null)
        //    {
        //        return result;
        //    }

        //    return null!;
        //}

        //[HttpGet]
        //[Route("/programDetails")]
        //public async Task<ProgramDetailsEntity> GetProgramDetails(int id)
        //{
        //    var result = await _courseDetailsRepository.GetProgramsDetails(id);

        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    return null!;
        //}
    }
}
