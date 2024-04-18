using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.ViewModels;
using System.Reflection;
using Infrastructure.Services;
using System.Net.Http.Headers;
using System.Net.Http;
using Infrastructure.Helpers;

namespace Infrastructure.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactServices _contactServices;
        private readonly GetTokenAndApiKey _getTokenAndApiKey;
        private readonly HttpClient _httpClient;

        public ContactController(ContactServices contactServices, GetTokenAndApiKey getTokenAndApiKey)
        {
            _contactServices = contactServices;
            _getTokenAndApiKey = getTokenAndApiKey;
        }

        private void setValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        [Route("/contact")]
        public IActionResult Index()
        {
            setValues();
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> Message(MessageViewModel viewModel)
        {
            setValues();

            if (ModelState.IsValid)
            {
                    var apiKey = _getTokenAndApiKey.GetApiKeyHelper(HttpContext);

                    var model = new ContactMessageModel
                    {
                        Name = viewModel.Message.Name,
                        Email = viewModel.Message.Email,
                        Message = viewModel.Message.Message,
                        Service = viewModel.Message.Service,
                    };

                    var result = await _contactServices.SendMessage(model, apiKey);
                    if(result == true)
                    {
                    TempData["ThanksForYourMessage"] = "Thanks for your Message!";
                    return RedirectToAction("Index");
                    }

                TempData["ErrorMessage"] = "Something went wrong...";
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task <IActionResult> Application(MessageViewModel model)
        {
            setValues();

            if (ModelState.IsValid)
            {
                var apiKey = _getTokenAndApiKey.GetApiKeyHelper(HttpContext);

                var contactModel  = new ContactCareersModel
                    {
                        Name = model.Application.Name,
                        Email = model.Application.Email,
                        Message =model.Application.Message,
                        Career = model.Application.Career,
                    };

                    var result = await _contactServices.SendApplication(contactModel, apiKey);

                    if(result == true)
                    {
                        TempData["ThanksForYourApplication"] = "Thanks for your Application!";
                        return RedirectToAction("Index");
                    }
            }

            return View("Index", model);
        }

    }
}
