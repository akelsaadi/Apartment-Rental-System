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
    class TEAM4OARSPrincipal : IPrincipal
    {
        private readonly TEAM4OARSIdentity teamIdentity;

        public TEAM4OARSPrincipal(TEAM4OARSIdentity _teamIdentity)
        {
            teamIdentity = _teamIdentity;
        }

        public IIdentity Identity
        {
            get { return teamIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}
