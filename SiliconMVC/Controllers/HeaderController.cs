using Infrastructure.Entities;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class HeaderController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public HeaderController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (_signInManager.IsSignedIn(User))
            {
                var userEntity = await _userManager.GetUserAsync(User);

                var viewModel = new AccountDetailsViewModel
                {
                    User = userEntity!,
                };

                return View(viewModel);
            }
            return View();
        }
    }
}
