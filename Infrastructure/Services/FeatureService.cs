using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Factories;

namespace Infrastructure.Services
{
    public class FeatureService
    {
        private readonly FeatureRepository _featureRepository;
        private readonly FeatureItemRepository _featureItemRepository;

        public FeatureService(FeatureRepository featureRepository, FeatureItemRepository featureItemRepository)
        {
            _featureRepository = featureRepository;
            _featureItemRepository = featureItemRepository;
        }

        public async Task<RepositoriesResult> GetAllFeatures()
        {
            try 
            {
                var result = await _featureRepository.GetAllAsync();

                if(result != null)
                {
                    return result;
                }

                return ResponseFactory.NotFound("No list found");
            }
            catch (Exception ex) 
            {
                return ResponseFactory.Error("GetAllFeaturesService" + ex.Message);
            }
        }
    }
}
