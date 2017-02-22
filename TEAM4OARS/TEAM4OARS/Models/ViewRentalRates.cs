using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TEAM4OARS.Models
{
    /*Author: Bertol Salgado 1361242
    * COSC 4351
    * Spring 2016
    * Model Class for Rental Rates
    */
    public class ViewRentalRates
    {
        [Display(Name ="Type ID")]
        public int Apt_Type { get; set; }

        [Display(Name ="Type")]
        public string Apt_Type_Desc { get; set; }

        [Display(Name = "Rental Rate")]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public Nullable<decimal> Apt_Rent_Amt { get; set; }
    }
}
