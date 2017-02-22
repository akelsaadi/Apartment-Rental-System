using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Script.Serialization;

namespace TEAM4OARS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookies.Value);
                JavaScriptSerializer js = new JavaScriptSerializer();
                TEAM4OARSRoleProvider role = new TEAM4OARSRoleProvider();
                string[] roleUser = role.GetRolesForUser(ticket.Name);

                if (roleUser[0] == "Tenant")
                {
                    Models.Tenant tenant = js.Deserialize<Models.Tenant>(ticket.UserData);
                    TEAM4OARSIdentity tenantIdentity = new TEAM4OARSIdentity(tenant);
                    TEAM4OARSPrincipal tenantPricipal = new TEAM4OARSPrincipal(tenantIdentity);
                    HttpContext.Current.User = tenantPricipal;
                }
                else
                {
                    Models.Staff staff = js.Deserialize<Models.Staff>(ticket.UserData);
                    TEAM4OARSIdentity staffIdentity = new TEAM4OARSIdentity(staff);
                    TEAM4OARSPrincipal staffPricipal = new TEAM4OARSPrincipal(staffIdentity);
                    HttpContext.Current.User = staffPricipal;
                }
            }
        }
    }
}
