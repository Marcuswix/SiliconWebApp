using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace SiliconMVC.Controllers
{
    [Authorize(Policy = "Admins")]
    public class AdminSubscribeController : Controller
    {
        private readonly GetTokenAndApiKey _getTokenAndApiKey;
        private readonly SubscribeServices _adminSubscribeServices;

        public AdminSubscribeController(GetTokenAndApiKey getTokenAndApiKey, SubscribeServices adminSubscribeServices)
        {
            _getTokenAndApiKey = getTokenAndApiKey;
            _adminSubscribeServices = adminSubscribeServices;
        }

        private void SetDefaultViewValues()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
        }

        [HttpGet]
        [Route("/admin/subscribers")]
        public async Task<IActionResult> Index()
        {
            SetDefaultViewValues();

            var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

            var response = await _adminSubscribeServices.GetAllSubscribers(token, apiKey);

            if (response != null && response.Count() > 0)
            {
                    return View(response);
            }
            if(response != null && response.Count() == 0)
            {
                TempData["ErrorMessage"] = "There are no subscribers in the list...";
            }
            else
            {
                TempData["ErrorMessage"] = "No response from API";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            SetDefaultViewValues();

            if (email != null)
            {
                var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);


                var response = await _adminSubscribeServices.Delete(email, token, apiKey);

                if (response.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    TempData["SuccessMessage"] = "The email is off from subscription";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    TempData["ErrorMessage"] = "Not found: ";
                }
                else if(response.StatusCode == Infrastructure.Models.StatusCodes.UNAUTHORIZED)
                {
                    TempData["ErrorMessage"] = "You are unauthorized to do this action";
                }
                else
                {
                    TempData["ErrorMessage"] = "No response from API";
                }
            }

            TempData["ErrorMessage"] = "Something went wrong";
            return RedirectToAction("Index");
        }


    }
}
