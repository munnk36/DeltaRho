using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace DeltaRhoPortal.Models
{
    //TODO refactor to just use 
    public class ManageViewModel
    {
        public status Status { get; set; }
        public string Student_Pdid { get; set; }
        public string Full_Name { get; set; }
        public string Nickname { get; set; }
        public string Preferred_Pronouns { get; set; }
        public string Major_Minor { get; set; }
        public DateTime? Birthday { get; set; }
        public string Brother_Bio { get; set; }
        public DateTime? Expected_Graduation { get; set; }
    }
}