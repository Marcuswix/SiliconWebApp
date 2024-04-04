using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Controllers;

public class HomeController : Controller
{
    private readonly SubscribeServices _subscribeServices;

    public HomeController(SubscribeServices subscribeServices)
    {
        _subscribeServices = subscribeServices;
    }

    private void SetValues()
    {
        ViewBag.ShowFooter = true;
        ViewBag.ShowChoices = true;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        SetValues();
        return View();
    }

    [HttpPost]
    public async Task <IActionResult> Subscribe(SubscribeModel model)
    {
        SetValues();
        
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await http.PostAsync("https://localhost:7117/api/Subscribe?key=920344b7-dd86-4721-9ce0-92e80f7d7da4", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Your are now subscribing!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["ErrorMessage"] = "This email address is already registerd for subscription.";
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try agin later.";
            }
        }
        return View("Index", model);
    }
}
