using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SiliconMVC.Controllers
{
    public class SiteSettings : Controller
    {
        public IActionResult ChangeTheme(string theme)
        {
            var option = new CookieOptions
            {
                //Anger hur länge denna kakan är giltlig.
                Expires = DateTime.Now.AddDays(60),
            };
            Response.Cookies.Append("ThemeMode", theme, option);
            return Ok();
        }

        [HttpPost]
        public IActionResult CookieConsent()
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(6),
                HttpOnly = true,
                Secure = true
            };
            Response.Cookies.Append("CookieConsent", "true", option);
            return Ok();

        }
    }
}
