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
        public static List<point_type> GetPointTypePermissions(string pdid) {
            List<point_type> canEdit = new List<point_type>();
            using (var context = new Entities()) {

                //yup, I know this looks ugly.
                //essentially: get the points type which map to either a
                //executive board officer position or executive council
                //chairperson position which the user member is currently
                //serving a term as.
                canEdit = context.Database.SqlQuery<point_type>(
                    @"SELECT * FROM point_type
                    WHERE point_type.point_type_id IN
	                    (SELECT point_type_id FROM point_type_permission
	                    WHERE point_type_permission.officer_title_id IN
		                    (SELECT officer_title_id FROM executive_board
		                    WHERE executive_board.position_id IN
			                    (SELECT position_id FROM leader
			                    WHERE leader.begin_term <= GETDATE() AND leader.end_term >= GETDATE() AND leader.member_id IN
				                    (SELECT member_id FROM member
				                    WHERE member.student_pdid = " + pdid + @"
				                    )
			                    )
		                    )
	                    UNION
	                    (SELECT point_type_id FROM point_type_permission
	                    WHERE point_type_permission.committee_name_id IN
		                    (SELECT committee_name_id FROM executive_council
		                    WHERE executive_council.position_id IN
			                    (SELECT position_id FROM leader
			                    WHERE leader.begin_term <= GETDATE() AND leader.end_term >= GETDATE() AND leader.member_id IN
				                    (SELECT member_id FROM member
				                    WHERE member.student_pdid = " + pdid + @"
				                    )
			                    )
		                    )
	                    )
                    )"
                ).ToList();
            }
            return canEdit;
        }
    }
}
