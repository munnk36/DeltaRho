using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeltaRhoPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool AccountIsRegistered { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class MemberPermissionsModel
    {
        public bool isMember()
        {
            return false;
        }

        public bool isOfficer()
        {
            return false;
        }

        public List<point_type_permission> canManagePointType()
        {
            return null;
        }
    }
}
