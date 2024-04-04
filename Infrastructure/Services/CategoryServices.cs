using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class CategoryServices
    {
        private readonly HttpClient _httpClient;

        public CategoryServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<RepositoriesResult> getAllCategories()
        //{
        //    try
        //    {
        //        var categories = _httpClient.GetAsync()
        //    }
        //    catch (Exception ex) { }
        //}
    }
}
