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
    
    public partial class point
    {
        public int point_id { get; set; }
        public int earned_by { get; set; }
        public int point_type_id { get; set; }
        public int logged_by { get; set; }
        public Nullable<int> event_attended { get; set; }
        public string criteria_met { get; set; }
        public Nullable<System.DateTime> date_given { get; set; }
        public Nullable<decimal> amount { get; set; }
    
        public virtual brother brother { get; set; }
        public virtual brother brother1 { get; set; }
        public virtual planned_event planned_event { get; set; }
        public virtual point_type point_type { get; set; }
    }
}
