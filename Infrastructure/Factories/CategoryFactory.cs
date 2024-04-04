using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Factories
{
    public class CategoryFactory
    {
        public static CategoryModel Create(CategoryEntity entity)
        {
            try
            {
                return new CategoryModel
                {
                    CategoryName = entity.CategoryName,
                };
            }
            catch { }
            return null!;
        }

        public static IEnumerable<CategoryModel> Create(List<CategoryEntity> entities)
        {
            try
            {
                List<CategoryModel> categories = [];
                foreach (var entity in entities) 
                {
                    categories.Add(Create(entity));
                }
                return categories;
            }
            catch { }
            return null!;
        }
    }
}
