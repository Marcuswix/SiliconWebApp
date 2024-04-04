using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Factories;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FeatureRepository(DataContext dataContext) : BaseRepositoriesPopulateWebbInfo<FeatureEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;

        public override async Task<RepositoriesResult> GetAllAsync()
        {
            try 
            {
                IEnumerable<FeatureEntity> result = await _dataContext.Features
                    .Include(i => i.FeatureItems)
                    .ToListAsync();

                return ResponseFactory.Ok(result);
            }
            catch (Exception ex){ Debug.WriteLine("GetAllAsyncFeatureEntity" + ex.Message);
                return ResponseFactory.Error(ex.Message); 
            }
        }
    }
}
