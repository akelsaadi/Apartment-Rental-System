using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace TEAM4OARS
{
    public class TEAM4OARSIdentity:IIdentity
    {
        public IIdentity Identity { get; set; }
        public Models.Tenant tenant { get; set; }

        public Models.Staff staff { get; set; }

        public TEAM4OARSIdentity(Models.Tenant user)
        {
            Identity = new GenericIdentity(user.Tenant_Username);
            tenant = user; ;
        }

        public TEAM4OARSIdentity(Models.Staff user)
        {
            Identity = new GenericIdentity(user.Username);
            staff = user; ;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}
