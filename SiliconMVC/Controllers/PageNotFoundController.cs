using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    public class PageNotFoundController : Controller
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
            return View();
        }

        [Route("/denied")]
        [HttpGet]
        public IActionResult Denied()
        {
            ViewBag.ShowFooter = false;
            ViewBag.ShowChoices = false;
            return View();
        }


        public IActionResult ReturnToHome() 
        {
            return RedirectToAction("Index", "Home");
        }
    }

    //public class DefaultController : Controller
    //{
    //    [Route("/")]
    //    public IActionResult Index() => View();


    //    [Route("/error")]
    //    public IActionResult Error404(int statusCode) => View();
    //}
}
