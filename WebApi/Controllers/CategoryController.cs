using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();

                if(categories != null)
                {
                    return Ok(categories);
                }
                return NoContent();
            }
            catch (Exception ex) {
                return NoContent();
            }
        }
        [HttpGet]
        [Route("/GetACategory")]
        public async Task<IActionResult> GetACategory(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetACategory(categoryId);

                if (category != null)
                {
                    return Ok(category);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
