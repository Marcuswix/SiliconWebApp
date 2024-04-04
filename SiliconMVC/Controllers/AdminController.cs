using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    [Authorize(Policy = "Admins")]
    //[Authorize(Roles = "Admin")] //Detta gör så bara de som har roll som admin kommer åt denna sida...
    public class AdminController : Controller
    {
        private void SetValues()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
        }

        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            SetValues();
            return View();
        }

        [Authorize(Policy = "CIO")]
        public IActionResult Settings() 
        {
            SetValues();
            return View();
        }
    }
}
