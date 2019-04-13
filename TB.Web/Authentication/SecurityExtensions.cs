using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;


namespace TB.Web.Authentication
{
    public static class SecurityExtensions
    {
        public static CustomPrincipal ToAppPrincipal(this IPrincipal principal)
        {
            return (CustomPrincipal)principal;
        }
    }
}