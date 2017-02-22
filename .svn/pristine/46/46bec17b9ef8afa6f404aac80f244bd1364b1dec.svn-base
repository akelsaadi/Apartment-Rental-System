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
    * Model class for Login Credentials
    */
    public class LoginModel
    {
        [Display(Name ="Username:")]
        [Required(ErrorMessage ="Please Provide a Username")]
        public string Username { get; set; }
        [Display(Name = "Password:")]
        [Required(ErrorMessage ="Please Provide a Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
