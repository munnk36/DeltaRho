using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using DeltaRhoPortal.Models;
using System;

namespace DeltaRhoPortal.Controllers {
    [RequireHttps]
    public class HomeController : Controller {
        public ActionResult Index() {
            HomeViewModel viewmodel = new HomeViewModel();
            if (Request.IsAuthenticated) {
                var userClaims = User.Identity as ClaimsIdentity;
                string pdid = "";
                string name = "";
                foreach (var claim in ((ClaimsIdentity)User.Identity).Claims) {
                    //get claim with pdid@bears.unco.edu called preferred_username
                    if (String.CompareOrdinal(claim.Type, "preferred_username") == 0) {
                        //get the first part of the pdid
                        pdid = claim.Value.Substring(0, claim.Value.IndexOf('@'));
                        ViewBag.StudentPdid = pdid;
                        break;
                    }
                    if (string.CompareOrdinal(claim.Type, "name") == 0) {
                        name = claim.Value;
                        ViewBag.MemberName = name;
                    }
                }
                if (String.IsNullOrWhiteSpace(pdid)) {
                    return RedirectToAction("Index", "Manage");
                }
                viewmodel.UserMember = UserMemberBridgeModel.getMemberFromPdid(pdid);
                if (viewmodel.UserMember != null) {

                }
                else {
                    return RedirectToAction("Index", "Manage");
                }
            }
            return View(viewmodel);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Send an OpenID Connect sign-in request.
        /// Alternatively, you can just decorate the SignIn method with the [Authorize] attribute
        /// </summary>
        public void SignIn() {
            if (!Request.IsAuthenticated) {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "../Home/Index" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut() {
            HttpContext.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}