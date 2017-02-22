﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace TEAM4OARS
{
    public class TEAM4OARSRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var cachekey = String.Format("{0}_role", username.Replace(" ",""));
            if(HttpRuntime.Cache[cachekey]!=null)
            {
                return (string[])HttpRuntime.Cache[cachekey];
            }

            string[] roles = new string[] { };

            using (Models.TEAM4OARSEntities dc = new Models.TEAM4OARSEntities())
            {              
                var tenant = dc.Tenants.Where(a => a.Tenant_Username.Equals(username)).FirstOrDefault();
                if(tenant!=null)
                {
                    string[] roletenant = new string[1] { "Tenant" };
                    roles= roletenant;
                }
                else
                {
                    var staff = dc.Staffs.Where(a => a.Username.Equals(username)).FirstOrDefault();
                    if(staff!=null)
                    {
                        string[] rolestaff = new string[1] { staff.Position.Replace(" ","") };
                        roles = rolestaff;
                    }
                }

                if (roles.Count() > 0)
                {
                    HttpRuntime.Cache.Insert(cachekey, roles, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                }
            }

            return roles;
                
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
