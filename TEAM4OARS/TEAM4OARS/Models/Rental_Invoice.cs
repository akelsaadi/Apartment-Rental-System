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
    
    public partial class Rental_Invoice
    {
        public int Invoice_No { get; set; }
        public Nullable<System.DateTime> Invoice_Date { get; set; }
        public Nullable<decimal> Invoice_Due { get; set; }
        public string CC_No { get; set; }
        public string CC_Type { get; set; }
        public Nullable<System.DateTime> CC_Exp_Date { get; set; }
        public Nullable<decimal> CC_Amt { get; set; }
        public Nullable<int> Rental_No { get; set; }
    
        public virtual Rental Rental { get; set; }
    }
}
