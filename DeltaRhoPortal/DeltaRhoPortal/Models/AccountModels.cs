using System;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;


namespace DeltaRhoPortal.Models {
    public class ExternalLoginConfirmationViewModel {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool AccountIsRegistered { get; set; }
    }

    public class ExternalLoginListViewModel {
        public string ReturnUrl { get; set; }
    }

    //performs database utility functions which associate a member
    //with the logged in user.
    public static class UserMemberBridgeModel {
        public static member getMemberFromPdid(string pdid) {
            using (var context = new Entities()) {
                var query = from m in context.members
                            where m.student_pdid == pdid
                            select m;
                return (query.FirstOrDefault());
            }
        }
        //when a user logins (presumably for the first time) with a
        //pdid unrecognized by the database add a new member object
        //to the database.
        public static void InsertNewGuestMember(string pdid, string name) {
            var context = new Entities();
            member guestUser = new member { student_pdid = pdid, full_name = name };
            context.members.Add(guestUser);
            context.SaveChanges();
        }

        public static List<point_type> GetLeaderPointTypePermissions(member userMember) {
            List<point_type> canEdit = new List<point_type>();

            if (userMember != null) {
                var context = new Entities();
            }

            return canEdit;
        }
        /// <summary>
        /// Note this will only update userMember, NOT it's associated
        /// table's columns. TODO: explicitly add sub-object to context to be updated
        /// </summary>
        /// <param name="userMember"></param>
        public static void SaveUserModel(member userMember) {
            using (var context = new Entities()) {
                var recordToUpdate = from m in context.members
                                     where m.member_id == userMember.member_id
                                     select m;
                member old = recordToUpdate.FirstOrDefault();
                if (userMember == null) {
                    return;
                }
                else if (old == null) {
                    InsertNewGuestMember(userMember.student_pdid, userMember.full_name);
                    return;
                }
                context.Entry(old).CurrentValues.SetValues(userMember);
                context.SaveChanges();
            }
        }
    }
}
