using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;


namespace TEAM4OARS.Controllers
{
    public class TEAM4OARSAccountController : Controller
    {
        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Login, Logout and register actions to access TEAM4OARS Website
        */
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.LoginModel login)
        {

            if (ModelState.IsValid)
            {
                bool isValidUser = Membership.ValidateUser(login.Username, login.Password);

                if (isValidUser)
                {
                    Models.Tenant tenant = null;
                    Models.Staff staff = null;
                    using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
                    {
                        tenant = dc.Tenants.Where(a => a.Tenant_Username.Equals(login.Username)).FirstOrDefault();
                        staff = dc.Staffs.Where(a => a.Username.Equals(login.Username)).FirstOrDefault();
                        if (tenant != null)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string data = js.Serialize(tenant);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, tenant.Tenant_Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, data);
                            string encToken = FormsAuthentication.Encrypt(ticket);
                            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                            Response.Cookies.Add(authCookies);
                            Session["Menu"] = null;
                            Session["Role"] = "Tenant";
                            return RedirectToAction("DisplayTenantIntro", "Home");
                        }
                        else if(staff!=null)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string data = js.Serialize(staff);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, staff.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, data);
                            string encToken = FormsAuthentication.Encrypt(ticket);
                            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                            Response.Cookies.Add(authCookies);
                            Session["Menu"] = null;
                            Session["Role"] = staff.Position.Replace(" ", "");
                            return RedirectToAction("StaffIndex", "Home");
                        }
                    }

 
                }
            }

            ModelState.Remove("Password");
            ModelState.AddModelError("Login Error", "Invalid Username or Password");
            return View();
        }

        
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Menu"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            List<SelectListItem> gender = new List<SelectListItem>();
            gender.Add(new SelectListItem { Text = "Male", Value = "M" });
            gender.Add(new SelectListItem { Text = "Female", Value = "F" });

            ViewBag.Gender = gender;

            List<SelectListItem> marital = new List<SelectListItem>();
            marital.Add(new SelectListItem { Text = "Single", Value = "S" });
            marital.Add(new SelectListItem { Text = "Married", Value = "M" });
            ViewBag.Marital = marital;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.Tenant t)
        {
            if (ModelState.IsValid)
            {
                using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
                {
                    dc.Tenants.Add(t);
                    dc.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                return View();
            }
        }
    }
}