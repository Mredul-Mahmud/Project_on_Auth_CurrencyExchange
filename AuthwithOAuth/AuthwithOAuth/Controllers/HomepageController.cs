using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthwithOAuth.Controllers
{
    public class HomepageController : Controller
    {
        // GET: Homepage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserHome()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}