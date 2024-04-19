using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Entities;
using Infrastructure.ViewModels;

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
        public async Task<IActionResult> Index(SubscriberEntity entity)
        {
            SetDefaultViewValues();

            var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

            var response = await _adminSubscribeServices.GetAllSubscribers(token, apiKey);

            if (response != null && response.Count() > 0)
            {
                var model = new SubscriberViewModel
                {
                    SubscriberEntity = entity != null ? entity : null,
                    Subscribers = response
                };

                return View(model);
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
                    TempData["ErrorMessage"] = "couldn't find any email that matched";
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

        [HttpPost]
        public async Task<IActionResult> GetOne(string email)
        {
            SetDefaultViewValues();

            if (email != null)
            {
                var (token, apiKey) = _getTokenAndApiKey.GetTokenAndApiKeyHelper(HttpContext);

                var response = await _adminSubscribeServices.GetOne(email, token, apiKey);

                if (response.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    var result = response.ContentResult as SubscriberEntity;

                    return RedirectToAction("Index", result);
                }
                else if (response.StatusCode == Infrastructure.Models.StatusCodes.NOT_FOUND)
                {
                    TempData["ErrorMessage"] = "Not found: ";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == Infrastructure.Models.StatusCodes.UNAUTHORIZED)
                {
                    TempData["ErrorMessage"] = "You are unauthorized to do this action";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "No response from API";
                    return RedirectToAction("Index");
                }
            }
            if(email == null)
            {
                TempData["ErrorMessage"] = "You must enter a email address...";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong";
            return RedirectToAction("Index");
        }
    }
}
