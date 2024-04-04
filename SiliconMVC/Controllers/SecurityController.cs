using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Entities;

namespace SiliconMVC.Controllers
{

    [Authorize]
    public class SecurityController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public SecurityController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void SetDefaultViewValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        #region [HttpGet] ChangePassword
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
        public IActionResult ChangePasswordView()
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
        public IActionResult DeleteAccountView()
        {
            SetDefaultViewValues();

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }

            return View();
        }
        #endregion 


        #region [HttpPost] ChangePassword
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            SetDefaultViewValues();

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid && model != null)
            {

                if (model.NewPassword == model.ConfirmNewPassword)
                {
                    if (user != null && user.PasswordHash == model.Password)
                    {
                        user.PasswordHash = model.NewPassword;
                        return RedirectToAction("ChangePasswordView", "Account", model);
                    }
                }
                else
                {
                    TempData["ErrorMessagePassword"] = "Passwords do not match";
                }
            }
            return View(model);
        }
        #endregion   
    }
}
