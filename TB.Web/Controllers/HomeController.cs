using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TB.Business.Product;
using TB.Cache.Product;
using TB.Domain.BE;
using TB.Web.Authentication;

namespace TB.Web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                CustomIdentity customIdentity = null;
                if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                    customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                string username = string.Empty;
                if (customIdentity != null)
                    username = customIdentity.Name;

                //ProductBO productBO = new ProductBO(username);
                ProductCache cache = new ProductCache();
                List<Product> list = (List<Product>)cache.GetAvailableProducts(username);//productBO.GetCampains();


                return View(list.Where(x =>x.IsCampain ==true).ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
            string cacheKey = string.Format("UserRoles_{0}", customIdentity.Name);
            FormsAuthentication.SignOut();
            HttpRuntime.Cache.Remove(cacheKey);
            return RedirectToAction("Index", "Login", null);
        }
    }
}