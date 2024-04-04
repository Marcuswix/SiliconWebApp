
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class IntegrateItemRepository : BaseRepositoriesPopulateWebbInfo<IntegrateItemRepository>
    {
        public IntegrateItemRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
