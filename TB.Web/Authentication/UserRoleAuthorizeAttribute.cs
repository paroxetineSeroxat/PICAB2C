using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace TB.Web.Authentication
{
    public class UserRoleAuthorizeAttribute : AuthorizeAttribute
    {
        public UserRoleAuthorizeAttribute() { }

        public UserRoleAuthorizeAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }
    public enum UserRole
    {
        Administrator = 1,
        User = 2
    }
}