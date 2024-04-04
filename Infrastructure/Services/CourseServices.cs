using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class CourseServices
    {
        private readonly CategoryRepository _categoryRepository;

        public CourseServices(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
