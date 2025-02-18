using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Facebook;

namespace AuthwithOAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 🔹 Step 1: Enable Cookie Authentication FIRST
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login") // Redirect to login if not authenticated
            });

            // 🔹 Step 2: Enable Google Authentication
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "507201411679 - q2rnsdtork5q1i32u5ebv7iu5rhnscu5.apps.googleusercontent.com",
                ClientSecret = "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",
                CallbackPath = new PathString("/Auth/GoogleCallback"),
                SignInAsAuthenticationType = "ApplicationCookie" // 🔹 Fixes your issue
            });
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "2291573021218621",
                AppSecret = "0ebbd17db346bae15f23f32761a84e26",
                CallbackPath = new PathString("/Auth/FacebookCallback"),
                SignInAsAuthenticationType = "ApplicationCookie",
                Scope = { "email" }
            });
        }
    }
}


