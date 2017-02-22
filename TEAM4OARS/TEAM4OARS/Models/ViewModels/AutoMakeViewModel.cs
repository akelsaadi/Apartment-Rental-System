using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TEAM4OARS.Models {
    public class AutoMakeViewModel {

        [Display(Name ="Auto Make")]
        [DataType(DataType.Text)]
        public string autoMake { get; set; }

        [Display(Name ="Count")]
        public int count { get; set; }
    }
}