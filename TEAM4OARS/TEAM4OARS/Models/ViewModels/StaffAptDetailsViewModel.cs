using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEAM4OARS.Models {
    public class StaffAptDetailsViewModel {
        public Staff staff { get; set; }
        public IEnumerable<int> aptNos { get; set; }
    }
}