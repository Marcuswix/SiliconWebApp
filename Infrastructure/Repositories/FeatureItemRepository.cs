using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class FeatureItemRepository : BaseRepositoriesPopulateWebbInfo<FeatureEntity>
    {
        public FeatureItemRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
