using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthwithOAuth.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public void LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback", "Auth", null, Request.Url.Scheme) };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
        }

        public async Task<ActionResult> GoogleCallback()
        {
            var loginInfo = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie").Result;
            if (loginInfo?.Identity == null)
            {
                return RedirectToAction("Login");
            }

            string email = loginInfo.Identity.FindFirst(ClaimTypes.Email)?.Value;
            string name = loginInfo.Identity.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(email))
            {
                Session["User"] = email;
                return RedirectToAction("UserHome", "Homepage");
            }
            return RedirectToAction("Login");
        }
       // [HttpPost]
        public ActionResult LoginWithFacebook()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("FacebookCallback", "Auth", null, Request.Url.Scheme) };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Facebook");
            return new HttpUnauthorizedResult();
        }

        public ActionResult FacebookCallback()
        {
            var loginInfo = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie").Result;
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            Session["User"] = loginInfo.Identity.Name;

            // Clear external login cookie
            HttpContext.GetOwinContext().Authentication.SignOut("ExternalCookie");

            return RedirectToAction("UserHome", "Homepage");
        }
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}