using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Entities;
using Infrastructure.Services;
using System.Net.Http.Headers;

namespace SiliconMVC.Controllers
{

    [Authorize]
    public class SecurityController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly HttpClient _httpClient;
        private readonly UserServices _userServices;
        private readonly IConfiguration _configuration;

        public SecurityController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, HttpClient httpClient, UserServices userServices, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpClient = httpClient;
            _userServices = userServices;
            _configuration = configuration;
        }

        public void SetDefaultViewValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        #region [HttpGet] Security
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {

            SetDefaultViewValues();

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }

            return View();
        }
        #endregion

        #region [HttpGet] ChangePassword
        [Authorize]
        [HttpGet]
        public IActionResult ChangePasswordView(PasswordModel model)
        {
            SetDefaultViewValues();

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }

            return View(model);
        }
        #endregion


        #region [HttpGet] DeleteAccount
        [Authorize]
        [HttpGet]
        public IActionResult DeleteAccountView(DeleteAccountModel model)
        {
            SetDefaultViewValues();

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }
            return View(model);
        }
        #endregion 


        #region [HttpPost] ChangePassword
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            SetDefaultViewValues();

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid && model != null && User != null)
            {

                if (model.NewPassword == model.ConfirmNewPassword)
                {
                    var changedPassword = await _userManager.ChangePasswordAsync(user!, model.Password, model.NewPassword);

                    if (changedPassword.Succeeded)
                    {
                        TempData["MessageSuccessSecurity"] = "Password was successfully updated!";
                        return RedirectToAction("Index", "Security");
                    }
                }
                else
                {
                    TempData["MessageErrorSecurity"] = "Passwords do not match...";
                }
            }

            TempData["MessageErrorSecurity"] = "Unable to save changes...";
            return View("Index");
        }
        #endregion

        #region [HttpDelete] DeleteAccount
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string api)
        {

            SetDefaultViewValues();

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiKey = _configuration["ApiKey:Secret"];

                if (!_signInManager.IsSignedIn(User))
                {
                    return RedirectToAction("SigIn", "Account");
                }

                var user = await _userManager.GetUserAsync(User);
                var result = await _userServices.DeleteUser(user!.Id, apiKey!);

                if (result.StatusCode == Infrastructure.Models.StatusCodes.OK)
                {
                    await _signInManager.SignOutAsync(); 
                    TempData["SuccessfullyDeleted"] = "The account was successfully deleted.";
                    return RedirectToAction("Index", "SignUp");
                }
            }

            return View("Index");
        }
        #endregion 
    }
}
