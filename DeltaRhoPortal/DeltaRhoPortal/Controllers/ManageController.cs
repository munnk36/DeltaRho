using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaRhoPortal.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        [Authorize]
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            string pdid = "";
            foreach (var claim in ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims)
            {
                //get claim with pdid@bears.unco.edu called preferred_username
                if (String.CompareOrdinal(claim.Type, "preferred_username") == 0)
                {
                    //get the first part of the pdid
                    pdid = claim.Value.Substring(0, claim.Value.IndexOf('@'));
                    ViewBag.StudentPdid = pdid;
                    break;
                }
            }
            return View();
        }
    }
}