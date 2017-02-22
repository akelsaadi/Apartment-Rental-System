﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TEAM4OARS.Common;
using TEAM4OARS.Models;

namespace TEAM4OARS.Controllers
{
    public class HomeController : Controller
    {
        /* setFeedbackMsg: Ben Wightman
        Used for feedback messages (e.g. success/failure) on input forms.
        To display on a view, use HTML like the following: 

        @if (TempData["msg"] != null) {
            <div class="@TempData["msgClass"]">@TempData["msg"]</div>
        }
        */
        private void setFeedbackMsg(string msg, string htmlClass) {
            TempData["msg"] = msg;
            TempData["msgClass"] = htmlClass;
        }
        
        #region TL Bertol Salgado

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display Home Page
        */
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display About Page
        */
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "TEAM4OARS Members";
            return View();
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display Contact Page
        */
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display testminonials Page
        */
        [AllowAnonymous]
        public ActionResult Testimonials(string keyWord)
        {
            using (Models.TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                ViewBag.Message = "Search a Testimonial";
                return View(dc.Testimonials.ToList());
            }
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display Chat Page
        */
        [AllowAnonymous]
        public ActionResult Chat()
        {
            TEAM4OARSRoleProvider rp = new TEAM4OARSRoleProvider();
            string[] roles = new string[1];
            ViewBag.Message = "Welcome to the Chat Room";
            if(User.Identity.IsAuthenticated)
            {
                roles = rp.GetRolesForUser(User.Identity.Name);
                ViewBag.Role = roles[0];
            }
            else
            {
                ViewBag.Role = "Visitor";
            }
            return View();
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display Staff Index Page
        */
        [TEAM4OARSAuthorize(Roles = "Assistant,Manager,Supervisor")]
        public ActionResult StaffIndex()
        {
            using (TEAM4OARSEntities dc= new TEAM4OARSEntities())
            {
                var results = dc.Database.SqlQuery<Models.ViewRentalRates>
                    ("SELECT DISTINCT Apt_Type, COALESCE(case when Apt_Type=0 then 'Studio' END,case when Apt_Type=1 then 'One Bedroom' END,case when Apt_Type=2 then 'Two Bedroom' END,case when Apt_Type=3 then 'Three Bedroom' END) as Apt_Type_Desc,Apt_Rent_Amt FROM Apartment").ToList<Models.ViewRentalRates>();
                return View(results);
            }
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display Apartment Rental Rates
        */
        [AllowAnonymous]
        public ActionResult AptRates()
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                var results = dc.Database.SqlQuery<Models.ViewRentalRates>
                    ("SELECT DISTINCT Apt_Type, COALESCE(case when Apt_Type=0 then 'Studio' END,case when Apt_Type=1 then 'One Bedroom' END,case when Apt_Type=2 then 'Two Bedroom' END,case when Apt_Type=3 then 'Three Bedroom' END) as Apt_Type_Desc,Apt_Rent_Amt FROM Apartment").ToList<Models.ViewRentalRates>();
                return View(results);
            }
        }



        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display all Vacant Apartments
        */
        [TEAM4OARSAuthorize(Roles = "Manager")]
        public ActionResult ListAptVacant()
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                return View(dc.View_VacantApt.ToList());
            }
        }

        /*Author: Bertol Salgado 1361242
        * COSC 4351
        * Spring 2016
        * Action use to display testimonials Results
        */
        [AllowAnonymous]
        public ActionResult TestimonialsResults(string keyword)
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                var parameter = new SqlParameter { ParameterName = "Keyword", Value = keyword.ToString() };
                var result = dc.Database.SqlQuery<Models.ViewTestimonialsResult>("exec sp_findTestimonial @Keyword", parameter).ToList<Models.ViewTestimonialsResult>();
                return View(result);
            }
        }

        [TEAM4OARSAuthorize(Roles ="Manager")]
        public ActionResult ListRentCollected()
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                var rentCollected = dc.Database.SqlQuery<ViewListRentCollected>
                    ("SELECT dateadd(month, x.MonthOffset,0) as [Month], SUM(x.cc_amt) as Amount FROM (SELECT datediff(month,0, inv.invoice_date) as MonthOffset, inv.cc_amt FROM rental_invoice inv) x GROUP BY MonthOffset ORDER BY MonthOffset").ToList<ViewListRentCollected>();
                return View(rentCollected);
            }
        }

        #endregion

        #region Garrett Bellomy

        /* Garrett Bellomy
         * 1128654
         * COSC 5351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles ="Manager,Assistant,Supervisor")]
        public ActionResult ListTenants()
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                return View(dc.View_Tenant.ToList());
            }
        }

        /* Garrett Bellomy
         * 1128654
         * COSC 5351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Manager,Assistant,Supervisor")]
        [HttpGet]
        public ActionResult ListPayments()
        {
            return View();
        }


        [TEAM4OARSAuthorize(Roles = "Manager,Assistant,Supervisor")]
        public ActionResult ListPaymentsResult(string number)
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                
                if(!String.IsNullOrEmpty(number))
                {
                    int apt = Convert.ToInt32(number);
                    var payments = dc.View_List_Payments.Where(s => s.apt_no.Equals(apt));
                    return View(payments.ToList());
                }
                else
                {
                    var payments = dc.View_List_Payments.ToList();
                    return View(payments);
                }
            }
        }

        /* Garrett Bellomy
         * 1128654
         * COSC 5351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Tenant")]
        public ActionResult LookupRentalAgreement()
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                var tenant = dc.Tenants.Where(s => s.Tenant_Username.Equals(User.Identity.Name)).FirstOrDefault();

                IEnumerable<Models.View_Rental> agreement = dc.View_Rental.Where(s => s.Tenant_SS.Equals(tenant.Tenant_SS));
                return View(agreement.ToList());
            }
        }

        /* Garrett Bellomy
         * 1128654
         * COSC 5351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles ="Supervisor")]
        public ActionResult ListStaffRentalDetails()
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                return View(dc.View_StaffRental_Details.ToList());
            }
                
        }

        #endregion

        #region Ali ElSaadi
        /* Ali Elsaadi
         * 1286957
         * COSC 4351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Tenant")]
        public ActionResult EnterComplaint()
        {
            return View();
        }

        [TEAM4OARSAuthorize(Roles = "Tenant")]
        [HttpPost]
        public ActionResult EnterComplaint(Models.Complaint c)
        {
            using (Models.TEAM4OARSEntities db = new Models.TEAM4OARSEntities())
            {
                var tenant = db.Tenants.Where(s => s.Tenant_Username.Equals(User.Identity.Name)).FirstOrDefault();
                var rental = db.Owns.Where(s => s.Tenant_SS == tenant.Tenant_SS).FirstOrDefault();
                c.Rental_No = rental.Rental_No;
                c.Apt_no = rental.Apt_No;
                c.Complaint_Date = DateTime.Now;
                c.Status = "P";
                if (ModelState.IsValid)
                {
                    db.Complaints.Add(c);
                    db.SaveChanges();
                    setFeedbackMsg("Complaint entered successfully", "text-success");
                    return View();
                }
                else
                {
                    setFeedbackMsg("There were errors. Please check the form.", "text-danger");
                    return View();
                }
            }

        }
        /* Ali Elsaadi
         * 1286957
         * COSC 4351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Supervisor")]
        public ActionResult ListComplaints()
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                var pending = dc.Complaints.Where(s => s.Status != "F");
                return View(pending.ToList());
            }
        }

        [TEAM4OARSAuthorize(Roles = "Supervisor")]
        public ActionResult UpdateComplaint(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                Complaint pc = dc.Complaints.Find(id);


                if (pc == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(pc);
                }
            }

        }

        [TEAM4OARSAuthorize(Roles = "Supervisor")]
        [HttpPost]
        public ActionResult UpdateComplaint([Bind(Include = "Complaint_No,Complaint_Date,Rental_Complaint,Apt_Complaint,Status,Rental_No,Apt_No")] Complaint pc)
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                if (ModelState.IsValid)
                {
                    dc.Entry(pc).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("ListComplaints", "Home");
                }
                return View(pc);
            }
        }
        /* Ali Elsaadi
         * 1286957
         * COSC 4351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Tenant")]
        public ActionResult EnterTestimonial()
        {
            return View();
        }

        [TEAM4OARSAuthorize(Roles = "Tenant")]
        [HttpPost]
        public ActionResult EnterTestimonial(Models.Testimonial newTestimonial)
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                var tenant = dc.Tenants.Where(s => s.Tenant_Username.Equals(User.Identity.Name)).FirstOrDefault();
                newTestimonial.Tenant_SS = tenant.Tenant_SS;
                newTestimonial.Testimonial_Date = DateTime.Now;

                if (ModelState.IsValid)
                {
                    dc.Testimonials.Add(newTestimonial);
                    dc.SaveChanges();
                    setFeedbackMsg("Testimonial entered successfully.", "text-success");
                    return View();

                }
                else
                {
                    setFeedbackMsg("There were errors. Please check the form.", "text-danger");
                    return View(newTestimonial);
                }
            }
        }

        /* Ali Elsaadi
         * 1286957
         * COSC 4351
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles = "Tenant")]
        public ActionResult DisplayTenantIntro()
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                var tenant = dc.Tenants.Where(s => s.Tenant_Username.Equals(User.Identity.Name)).FirstOrDefault();
                var tenantInfo = dc.Tenants.Where(s => s.Tenant_SS==tenant.Tenant_SS);
                var tenantAuto = dc.Tenant_Auto.Where(s => s.Tenant_SS==tenant.Tenant_SS);
                var tenantFamily = dc.Tenant_Family.Where(s => s.Tenant_SS==tenant.Tenant_SS);
                var rentalRates = dc.Database.SqlQuery<Models.ViewRentalRates>
                    ("SELECT DISTINCT Apt_Type, COALESCE(case when Apt_Type=0 then 'Studio' END,case when Apt_Type=1 then 'One Bedroom' END,case when Apt_Type=2 then 'Two Bedroom' END,case when Apt_Type=3 then 'Three Bedroom' END) as Apt_Type_Desc,Apt_Rent_Amt FROM Apartment").ToList<Models.ViewRentalRates>();

                var TenantData = new Tuple<List<Models.Tenant>, List<Models.Tenant_Auto>, List<Models.Tenant_Family>,List<ViewRentalRates>>(tenantInfo.ToList(),tenantAuto.ToList(),tenantFamily.ToList(),rentalRates);
                return View(TenantData);
            }
        }

        #endregion

        #region Ben Wightman

        /* Ben Wightman
         * 1012702
         * COSC 4530
         * Spring 2016
        */
        [TEAM4OARSAuthorize(Roles ="Manager")]
        [HttpGet]
        public ActionResult ListTenantAutoMake()
        {
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                var automakes = from m in dc.Tenant_Auto
                                group m by m.Auto_Make into gp
                                select new Models.AutoMakeViewModel
                                {
                                    autoMake = gp.Key,
                                    count = gp.Count()
                                };
                return View(automakes.ToList());
            }
        }

        [TEAM4OARSAuthorize(Roles = "Assistant")]
        [HttpGet]
        public ActionResult CreateRental()
        {
            return View();
        }

        [TEAM4OARSAuthorize(Roles = "Assistant")]
        [HttpPost]
        public ActionResult CreateRental(Models.CreateRentalViewModel vm)
        {
            string errormsg = "There were errors. Please double-check the form.";
            string successmsg = "Rental created successfully.";
            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {
                // Get the current logged-in staff member
                var staff = dc.Staffs.Where(s => s.Username.Equals(User.Identity.Name)).FirstOrDefault();

                if (staff == null) {
                    return RedirectToAction("Login", "TEAM4OARSAccount");
                }
                
                // SSN is the primary key. We need to enforce existence (if an existing tenant)
                // or uniqueness (if a new tenant).
                Models.Tenant t = dc.Tenants.Where(s => (s.Tenant_SS == vm.tenant.Tenant_SS)).FirstOrDefault();
                if (t == null && !vm.isNewTenant) {
                    ViewData.ModelState.AddModelError("tenant.Tenant_SS", "No tenant with the given SSN exists.");
                }
                else if(t != null && vm.isNewTenant) {
                    ModelState.AddModelError("tenant.Tenant_SS", "A tenant with this SSN already exists.");
                }

                // If we are adding a new tenant, do some extra validation checks on the registration info.
                // We only do this for new tenants - for existing ones, we just ignore all of these.
                if (vm.isNewTenant) {
                    Tenant t2 = dc.Tenants.Where(s => (s.Tenant_Username == vm.tenant.Tenant_Username)).FirstOrDefault();
                    if (t2 != null) {
                        ModelState.AddModelError("tenant.Tenant_Username", "This username is already in use.");
                    }

                    if(vm.tenant.Tenant_Name == null)
                        ModelState.AddModelError("tenant.Tenant_Name", "Required");
                    if (vm.tenant.Tenant_DOB == null)
                        ModelState.AddModelError("tenant.Tenant_DOB", "Required");
                    if (vm.tenant.Work_Phone == null)
                        ModelState.AddModelError("tenant.Work_Phone", "Required");

                    if (vm.tenant.Tenant_Username == null)
                        ModelState.AddModelError("tenant.Tenant_Username", "Username cannot be empty");
                    else if (vm.tenant.Tenant_Username.Length > 30)
                        ModelState.AddModelError("tenant.Tenant_Username", "Username must be 30 characters or less");

                    if (vm.tenant.Tenant_Password == null)
                        ModelState.AddModelError("tenant.Tenant_Password", "A password is required.");
                    else if (vm.tenant.Tenant_Password.Length < 8 || vm.tenant.Tenant_Password.Length > 30)
                        ModelState.AddModelError("tenant.Tenant_Password", "Password must be between 8 and 30 characters.");


                    if (vm.confirmPasswd == null)
                        ModelState.AddModelError("confirmPasswd", "Please retype password to confirm it.");
                    else if (!vm.confirmPasswd.Equals(vm.tenant.Tenant_Password))
                        ModelState.AddModelError("confirmPasswd", "Passwords do not match.");
                }
                
                // Make sure the given apartment actually exists before renting it out
                Models.Apartment apt = dc.Apartments.Where(s => (s.Apt_no == vm.aptNo)).FirstOrDefault();
                if (apt == null) {
                    ModelState.AddModelError("aptNo", "No apartment with this number exists.");
                }

                // Is there an existing rental of this apartment? If so, don't let them choose it

                Models.Rental rentalCheck = dc.Rentals.Where(s => s.Apt_no == vm.aptNo && s.Lease_End > DateTime.Today).FirstOrDefault();
                if (rentalCheck != null)
                    ModelState.AddModelError("aptNo", "This apartment is already rented");

                // If there were any errors above, bail out now and serve the error message
                if (!ModelState.IsValid) {
                    setFeedbackMsg(errormsg, "text-danger");
                    return CreateRental();
                }

                Models.Rental r = new Models.Rental();
                Models.Own o = new Models.Own();
                
                // Fill in default information for rental
                r.Lease_Type = vm.Lease_Type;
                r.Cancel_Date = null; // rental hasn't been canceled yet
                r.Renewal_Date = null; // rental hasn't been renewed yet
                r.Rental_Date = r.Lease_Start = DateTime.Today;
                r.Rental_Status = "S";

                // Get lease end date, as either 1 or 6 months past today
                int months;
                if (r.Lease_Type.Equals("Six"))
                    months = 6;
                else
                    months = 12;
                r.Lease_End = DateTime.Today.AddMonths(months);
                r.Staff = staff;
                r.Apartment = apt;

                // Fill Owns information
                if (vm.isNewTenant)
                    o.Tenant = vm.tenant;
                else
                    o.Tenant = t;
                o.Rental = r;
                o.Apartment = apt;

                // If everything went smoothly, add the things to the database
                if (ModelState.IsValid) {
                    if(vm.isNewTenant) {
                        dc.Tenants.Add(vm.tenant);
                    }
                    dc.Rentals.Add(r);
                    dc.Owns.Add(o);
                    dc.SaveChanges();
                }

                setFeedbackMsg(successmsg, "text-success");
                return CreateRental();
            }
        }

        [TEAM4OARSAuthorize(Roles ="Supervisor")]
        public ActionResult ListStaffAptDetails() {
            using(TEAM4OARSEntities dc = new TEAM4OARSEntities()) {
                List<StaffAptDetailsViewModel> modelList = new List<StaffAptDetailsViewModel>();
                foreach(var st in dc.Staffs) {
                    StaffAptDetailsViewModel vm = new StaffAptDetailsViewModel();
                    vm.staff = st;
                    vm.aptNos = dc.Database.SqlQuery<int>("SELECT DISTINCT Apt_no FROM Rental WHERE Staff_number = @p0", st.Staff_number).ToList();

                    modelList.Add(vm);
                }
                return View(modelList);
            }
        }

        #endregion

        #region Joshua Wilburn

        [TEAM4OARSAuthorize(Roles = "Supervisor")]
        public ActionResult ListPendingComplaints()
        {
            using (Models.TEAM4OARSEntities dataConnection = new Models.TEAM4OARSEntities())
            {
                return View(dataConnection.View_Pending_Complaints.ToList());
            }
        }

        [TEAM4OARSAuthorize(Roles = "Manager")]
        public ActionResult ListLeases()
        {
            using (Models.TEAM4OARSEntities dataConnection = new Models.TEAM4OARSEntities())
            {
                return View(dataConnection.View_LeaseLength.ToList());
            }
        }

        [TEAM4OARSAuthorize(Roles="Tenant")]
        public ActionResult PayRent()
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                var tenant = dc.Tenants.Where(s => s.Tenant_Username.Equals(User.Identity.Name)).FirstOrDefault();
                var rental = dc.Owns.Where(s => s.Tenant_SS == tenant.Tenant_SS).FirstOrDefault();
                var apartment= dc.Apartments.Where(s => s.Apt_no == rental.Apt_No).FirstOrDefault();
                ViewPayRent pay = new ViewPayRent();
                pay.rates= dc.Database.SqlQuery<Models.ViewRentalRates>
                    ("SELECT DISTINCT Apt_Type, COALESCE(case when Apt_Type=0 then 'Studio' END,case when Apt_Type=1 then 'One Bedroom' END,case when Apt_Type=2 then 'Two Bedroom' END,case when Apt_Type=3 then 'Three Bedroom' END) as Apt_Type_Desc,Apt_Rent_Amt FROM Apartment").ToList<Models.ViewRentalRates>();
                pay.Rental_No = rental.Rental_No;
                pay.Invoice_Due = apartment.Apt_Rent_Amt;
                //string today = DateTime.Now.ToShortDateString();
                pay.Invoice_Date = DateTime.Now;
                pay.Tenant_SS = tenant.Tenant_SS;
                pay.Tenant_Name = tenant.Tenant_Name;
                pay.Apt_no = apartment.Apt_no;
                return View(pay);
            }
        }

        [TEAM4OARSAuthorize(Roles ="Tenant")]
        [HttpPost]
        public ActionResult PayRent(Models.ViewPayRent pay)
        {
            using (TEAM4OARSEntities dc = new TEAM4OARSEntities())
            {
                Rental_Invoice ri = new Rental_Invoice();
                ri.Invoice_Date = pay.Invoice_Date;
                ri.Invoice_Due = pay.Invoice_Due;
                ri.CC_No = pay.CC_No;
                ri.CC_Type = pay.CC_Type;
                ri.CC_Exp_Date = pay.CC_Exp_Date;
                ri.CC_Amt = pay.CC_Amt;
                ri.Rental_No = pay.Rental_No;
                

                dc.Rental_Invoice.Add(ri);
                dc.SaveChanges();
            
                //Get invoice number
               pay.Invoice_No = ri.Invoice_No;
              
               ViewBag.Title = "Receipt";
            //Update Rental Invoice
            return View(pay);
        }

        }

       

        #endregion

    }
}