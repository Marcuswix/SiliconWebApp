using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class IntegrateRepository : BaseRepositoriesPopulateWebbInfo<IntegrateEntity>
    {
        private readonly DataContext _context;

        public IntegrateRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<RepositoriesResult> GetAllAsync()
        {
            try
            {
                IEnumerable<IntegrateEntity> result = await _context.Integrate.Include(i => i.IntegrateItems)
                    .ToListAsync();

                return ResponseFactory.Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllAsyncIntegrateEntity" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
