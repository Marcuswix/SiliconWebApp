using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using System.Text;
using Infrastructure.Helpers;

namespace Infrastructure.Controllers;

public class HomeController : Controller
{
    private readonly SubscribeServices _subscribeServices;
    private readonly GetTokenAndApiKey _tokenAndApiKey;

    public HomeController(SubscribeServices subscribeServices, GetTokenAndApiKey tokenAndApiKey)
    {
        _subscribeServices = subscribeServices;
        _tokenAndApiKey = tokenAndApiKey;
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
            var (token, apiKey) = _tokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

            var response = await _subscribeServices.AddSubscriber(model, apiKey);

            if (response.StatusCode == Infrastructure.Models.StatusCodes.OK)
            {
                TempData["Message"] = "Your are now subscribing!";
            }
            else if (response.StatusCode == Infrastructure.Models.StatusCodes.EXISTS)
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
