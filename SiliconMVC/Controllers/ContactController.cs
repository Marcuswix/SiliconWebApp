using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels;
using System.Reflection;

namespace Infrastructure.Controllers
{
    public class ContactController : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";
            return View();
        }


        
        [HttpPost]
        public IActionResult Message(MessageViewModel viewModel)
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid)
            {
                TempData["ThanksForYourMessage"] = "Thanks for your Message!";
                return RedirectToAction("Index");
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult Application(MessageViewModel model)
        {
            ViewBag.ShowFooter = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid) 
            {
                TempData["ThanksForYourApplication"] = "Thanks for your Application!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

    }
}
