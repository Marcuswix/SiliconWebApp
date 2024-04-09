using Infrastructure.Entities;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiliconMVC.Controllers
{
    public class SignUpController : Controller
    {

        private readonly UserManager<UserEntity> _userManager;

        public SignUpController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public void SetDefaultValues()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
        }

        #region [HttpGet] SignUp
        //SIGN-UP
        [HttpGet]
        [Route("/signup")]
        public IActionResult Index()
        {
            var viewModel = new SignUpViewModel();
            SetDefaultValues();
            return View(viewModel);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            SetDefaultValues();

            if (ModelState.IsValid)
            {
                var exist = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);

                if (exist == true)
                {
                    viewModel.ErrorMessage = "A user with the same email already exist";
                    return View("Index", viewModel);
                }

                var userEntity = new UserEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                    UserName = viewModel.Form.Email,
                };

                var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(userEntity, "User");

                    if(roleResult.Succeeded)
                    {
                        return RedirectToAction("Index", "SignIn");
                    }
                }
            }
            return View("Index", viewModel);
        }
    }
}
