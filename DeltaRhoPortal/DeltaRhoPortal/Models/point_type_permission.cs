//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeltaRhoPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class point_type_permission
    {
        public int point_type_permission_id { get; set; }
        public int point_type_id { get; set; }
        public Nullable<int> officer_title_id { get; set; }
        public Nullable<int> committee_name_id { get; set; }
    
        public virtual committee_names committee_names { get; set; }
        public virtual officer_title officer_title { get; set; }
        public virtual point_type point_type { get; set; }
    }
}