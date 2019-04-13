using TB.Web.Authentication;
using TB.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TB.Web
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

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity = new CustomIdentity(HttpContext.Current.User.Identity);
                var principal = new CustomPrincipal(identity);
                HttpContext.Current.User = principal;
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            /*
            Exception exception = Server.GetLastError();
            Server.ClearError();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");

            var currentController = " ";
            var currentAction = " ";

            try
            {
                var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context));

                if (currentRouteData != null)
                {
                    if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                        currentController = currentRouteData.Values["controller"].ToString();

                    if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                        currentAction = currentRouteData.Values["action"].ToString();
                };

                Response.StatusCode = 404;
                Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                Response.End();
            }
            catch (Exception)
            {
                //throw;
            }
            */
        }
    }
}
