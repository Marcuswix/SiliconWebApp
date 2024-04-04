
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class IntegrateService
    {
            private readonly IntegrateRepository _integrateRepository;
            private readonly IntegrateItemRepository _integrateItemRepository;

        public IntegrateService(IntegrateRepository integrateRepository, IntegrateItemRepository integrateItemRepository)
        {
            _integrateRepository = integrateRepository;
            _integrateItemRepository = integrateItemRepository;
        }

            public async Task<RepositoriesResult> GetAllIntegrate()
            {
                try
                {
                    var result = await _integrateRepository.GetAllAsync();

                    if (result != null)
                    {
                        return result;
                    }

                    return ResponseFactory.NotFound("No list found");
                }
                catch (Exception ex)
                {
                    return ResponseFactory.Error("GetAllIntegrateService" + ex.Message);
                }
            }
        }
}
