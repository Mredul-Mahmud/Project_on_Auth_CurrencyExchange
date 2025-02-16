using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoDbAuth.Controllers
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

        [HttpPost]
        public ActionResult Register(string email, string password)
        {
            HttpCookie userCookie = new HttpCookie("UserAuth")
            {
                ["Email"] = email,
                ["Password"] = password,
                Expires = DateTime.MaxValue
            };

            Response.Cookies.Add(userCookie);
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string token)
        {
            HttpCookie userCookie = Request.Cookies["UserAuth"];
            if (userCookie != null &&
                userCookie["Email"] == email &&
                userCookie["Password"] == password)
            {
                Session["User"] = email;
                return RedirectToAction("UserHome", "Homepage");
            }
            ViewBag.Error = "Please check your information again!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}