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
    
    public partial class point_type_grouping
    {
        public int type_grouping_id { get; set; }
        public Nullable<int> point_type_id { get; set; }
        public Nullable<int> event_id { get; set; }
    
        public virtual planned_event planned_event { get; set; }
        public virtual point_type point_type { get; set; }
    }
}
