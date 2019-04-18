using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatDogCore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn(string redirectUrl)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            }, 
            OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        //public IActionResult SignOut()
        //{
        //    return SignOut(new AuthenticationProperties
        //    {
        //        RedirectUri = "/"
        //    }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        //}
    }
}
