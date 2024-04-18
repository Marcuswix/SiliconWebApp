using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infrastructure.Helpers
{
    public class GetTokenAndApiKey
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GetTokenAndApiKey(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public (string, string) GetTokenAndApiKeyHelper(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var apiKey = _configuration["ApiKey:Secret"];
                    return (token, apiKey);
                }

                return (null, null);
            }
            catch
            {
                return (null, null);
            }
        }

        public string GetApiKeyHelper(HttpContext httpContext)
        {
            try
            {
                    var apiKey = _configuration["ApiKey:Secret"];
                    return (apiKey);
            }
            catch
            {
                return (null!);
            }
        }
    }
}
