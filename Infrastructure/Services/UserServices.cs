using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class UserServices
    {
        public async Task<RepositoriesResult> DeleteUser(string id, string apiKey)
        {
            try
            {
                if(id != null && apiKey != null)
                {
                    using var _http = new HttpClient();

                    var result = await _http.DeleteAsync($"https://localhost:7117/api/Security?id={id}&key={apiKey}");
                    
                    if(result.IsSuccessStatusCode)
                    {
                        return ResponseFactory.Ok();
                    }

                    return ResponseFactory.Error();
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ResponseFactory.Error();
            }
        }
    }
}
