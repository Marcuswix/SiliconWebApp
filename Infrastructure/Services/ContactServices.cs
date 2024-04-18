using Infrastructure.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ContactServices
    {

        public async Task<bool> SendMessage(ContactMessageModel model, string apiKey)
        {
            try
            {
                if (model != null)
                {
                    var http = new HttpClient();

                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var result = await http.PostAsync($"https://localhost:7117/api/Contact/message?key={apiKey}", content);

                    if(result.IsSuccessStatusCode)
                    {
                         return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendApplication(ContactCareersModel model, string apiKey)
        {
            try
            {
                if (model != null)
                {
                    var http = new HttpClient();

                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var result = await http.PostAsync($"https://localhost:7117/api/Contact/career?key={apiKey}", content);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
