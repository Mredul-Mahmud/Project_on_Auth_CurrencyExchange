using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NoDbAuthwOTP.Controllers
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
        public ActionResult Register(string email)
        {
            string otp = new Random().Next(100000, 999999).ToString();
            Session["OTP"] = otp;
            Session["User"] = email;

            SendOTP(email, otp);

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usersOtp)
        {
            string savedOtp = Session["OTP"]?.ToString();  // Safely access OTP from session
            string savedUser = Session["User"]?.ToString();  // Safely access user email

            if (savedOtp != null && savedOtp == usersOtp)
            {
                // OTP is valid, set authentication session
                Session["IsAuthenticated"] = true;
                return RedirectToAction("HomePage");
            }

            // If OTP is invalid, show error message
            ViewBag.Message = "Invalid OTP. Try again with the valid one.";
            return View();
        }
        public ActionResult HomePage()
        {
            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        private void SendOTP(string email, string otp)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dotnetsolution01@gmail.com", "pydbhhblbyyjyfjm"),
                EnableSsl = true,
            };

            smtpClient.Send("dotnetsolution01@gmail.com", email, "Your OTP Code", "Your OTP is: " + otp);
        }


    }
}