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

        public async Task<CategoryEntity> GetACategory(int categoryId)
        {
            try
            {
                var result = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

                if (result != null)
                {
                    return result;
                }
            }
            catch
            {

            }
            return null!;
        }

        public async Task<CategoryEntity> GetACategoryBySearch(string search)
        {
            try
            {
                var result = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == search);

                if (result != null)
                {
                    return result;
                }
            }
            catch
            {

            }
            return null!;
        }
    }
}
