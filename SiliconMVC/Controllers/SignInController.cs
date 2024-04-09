using Infrastructure.Entities;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Text;


namespace SiliconMVC.Controllers
{
    [AllowAnonymous]
    public class SignInController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SignInController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, HttpClient httpClient, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public void SetDefaultValues()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
        }

        #region [HttpGet] SignIn
        [HttpGet]
        [Route("/signin")]
        public IActionResult Index(string returnUrl)
        {
            SetDefaultValues();

            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }

            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }
        #endregion

        #region [HttpPost] SignIn
        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            SetDefaultValues();
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false);

                if (result.Succeeded)
                {
                    var login = new Dictionary<string, string>()
                    {
                        { "email", model.Form.Email }, { "password", model.Form.Password }
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                    var apiKey = _configuration["ApiKey:Secret"];

                    //Här så får vi en token-string som gör att vi kan logga in...
                    var response = await _httpClient.PostAsync($"https://localhost:7117/api/Auth/token?key={apiKey}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(1)
                        };

                        //Skickar med en cookie i webbläsaren("namn", variabel, "inställningar")
                        Response.Cookies.Append("AccessToken", token, cookieOptions);
                    }

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return LocalRedirect(returnUrl);

                }
            }
            else if(model.Form.Email == null || model.Form.Password == null)
            {
                return View("Index", model);
            }

            ModelState.AddModelError("IncorrextValues", "Incorrect email or password");
            ViewData["ErrorMessage"] = "Incorrect email or password";

            return View("Index", model);
        }
        #endregion



        #region External Account | Facebook
        [HttpGet]
        public IActionResult Facebook()
        {
            var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));

            return new ChallengeResult("Facebook", authProps);
        }
        #endregion

        #region [HttpGet] FacebookCallback
        [HttpGet]
        public async Task<IActionResult> FacebookCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info != null)
            {
                var userEntity = new UserEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    IsExternalAccount = true,
                };

                var user = await _userManager.FindByEmailAsync(userEntity.Email);

                if (user == null)
                {
                    var result = await _userManager.CreateAsync(userEntity);

                    if (result.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(userEntity.Email);
                    }
                }

                if (user != null)
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                    {
                        user.FirstName = userEntity.FirstName;
                        user.LastName = userEntity.LastName;
                        user.Email = userEntity.Email;
                        user.IsExternalAccount = true;

                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("InvalidFacebookAuthenication", "danger|Failed facebook authentication");
            ViewData["StatusMessage"] = "danger|Failed facebook authentication";
            return RedirectToAction("Index", "SignIn");
        }
        #endregion

        //#region [Post] Google login
        //[HttpGet]
        //public IActionResult Google()
        //{
        //    var authProps = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action(nameof(GoogleResponse)) // Använd nameof-operatorn för att undvika hårda koded URI:er
        //    };

        //    return Challenge(authProps, GoogleDefaults.AuthenticationScheme);
        //}
        //#endregion

        //[Route("google-response")]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    var claims = result.Principal.Identities.FirstOrDefault()
        //        .Claims.Select(claim => new
        //        {
        //            claim.Issuer,
        //            claim.OriginalIssuer,
        //            claim.Type,
        //            claim.Value
        //        });

        //    //return Json(claims);
        //    return RedirectToAction("Index", "Home", new { area = "" });
        //}
}
}