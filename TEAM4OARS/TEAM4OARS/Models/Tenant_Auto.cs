//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEAM4OARS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tenant_Auto
    {
        public string License_No { get; set; }
        public string Auto_Make { get; set; }
        public string Auto_Model { get; set; }
        public Nullable<int> Auto_Year { get; set; }
        public string Auto_Color { get; set; }
        public Nullable<int> Tenant_SS { get; set; }
    
        public virtual Tenant Tenant { get; set; }
    }
}
