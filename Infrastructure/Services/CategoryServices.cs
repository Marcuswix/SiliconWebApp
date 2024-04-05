using Infrastructure.Entities;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class CategoryServices
    {
        private readonly HttpClient _httpClient;

        public CategoryServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryEntity>> getAllCategories()
        {
            try
            {
                var categories = await _httpClient.GetAsync("https://localhost:7117/api/Category");

                if(categories.IsSuccessStatusCode)
                {
                    var json = await categories.Content.ReadAsStringAsync();
                    var sendBack = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);
                    return sendBack;
                }
                return null!;
            }
            catch (Exception ex) {
                return null!;
            }
        }
    }
}
