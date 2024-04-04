using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<CategoryEntity>> GetAllCategories()
        {
            try
            {
                var result = await _dataContext.Categories.OrderBy(x => x.CategoryName).ToListAsync();

                if(result != null)
                {
                    return result;
                }
            }
            catch {

            }
            return null!;
        }
    }
}
