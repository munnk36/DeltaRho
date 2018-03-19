using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeltaRhoPortal.Models;
using System.Security.Claims;

namespace DeltaRhoPortal.Controllers {
    public class ManageController : Controller {
        // GET: Manage
        [Authorize]
        public ActionResult Index() {
            ManageViewModel viewmodel = new ManageViewModel();
            var userClaims = User.Identity as ClaimsIdentity;
            string pdid = "";
            string name = "";
            foreach (var claim in ((ClaimsIdentity)User.Identity).Claims) {
                //get claim with pdid@bears.unco.edu called preferred_username
                if (String.CompareOrdinal(claim.Type, "preferred_username") == 0) {
                    //get the first part of the pdid
                    pdid = claim.Value.Substring(0, claim.Value.IndexOf('@'));
                    break;
                }
                if (string.CompareOrdinal(claim.Type, "name") == 0) {
                    name = claim.Value;
                }
            }
            if (String.IsNullOrWhiteSpace(pdid)) {
                return View(viewmodel);
            }
            member usermember = UserMemberBridgeModel.getMemberFromPdid(pdid);
            if (usermember != null) {
                ManageViewModel newviewmodel = new ManageViewModel() {
                    Status = usermember.status,
                    Student_Pdid = usermember.student_pdid,
                    Full_Name = usermember.full_name,
                    Nickname = usermember.nickname,
                    Preferred_Pronouns = usermember.preferred_pronouns,
                    Major_Minor = usermember.major_minor,
                    Birthday = usermember.birthday,
                    Brother_Bio = usermember.brother_bio,
                    Expected_Graduation = usermember.expected_graduation
                };
                viewmodel = newviewmodel;
            }
            ///make a new guest user if the member doesn't show up in our database
            else {
                UserMemberBridgeModel.InsertNewGuestMember(pdid, name);
            }
            return View(viewmodel);
        }

        //POST: Manage/Index
        //TODO Sanitation
        //TODO DRY
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = @"Student_Pdid, Nickname, Preferred_Pronouns
                                                    Major_Minor, Birthday, Brother_Bio
                                                    Expected_Graduation")] ManageViewModel viewmodel) {
            if (ModelState.IsValid) {
                string pdid = "";
                foreach (var claim in ((ClaimsIdentity)User.Identity).Claims) {
                    //get claim with pdid@bears.unco.edu called preferred_username
                    if (String.CompareOrdinal(claim.Type, "preferred_username") == 0) {
                        //get the first part of the pdid
                        pdid = claim.Value.Substring(0, claim.Value.IndexOf('@'));
                        break;
                    }
                }

                member usermember = UserMemberBridgeModel.getMemberFromPdid(pdid);

                //want to only modify non-null members that match authentication context
                if (usermember != null && String.CompareOrdinal(pdid, viewmodel.Student_Pdid) == 0) {
                    usermember.nickname = viewmodel.Nickname;
                    usermember.preferred_pronouns = viewmodel.Preferred_Pronouns;
                    usermember.major_minor = viewmodel.Major_Minor;
                    usermember.birthday = viewmodel.Birthday;
                    usermember.brother_bio = viewmodel.Brother_Bio;
                    usermember.expected_graduation = viewmodel.Expected_Graduation;

                    UserMemberBridgeModel.SaveUserModel(usermember);
                }
            }
            return View(viewmodel);
        }
    }
}