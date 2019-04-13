using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TB.Web.Authentication;
using TB.Web.Models;
using TB.Domain.Constants;
using System.Web.Security;

namespace TB.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var customIdentity = ControllerContext.HttpContext.User.Identity;
            if (customIdentity.Name == "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                string loginStatus = CustomMembershipProvider.ValidateUserActiveDirectory(model.Name, model.Password);
                if (loginStatus == LoginStatus.OK)
                {
                    FormsAuthentication.RedirectFromLoginPage(model.Name, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.message = loginStatus;
                    return View(model);
                }
            }
        }


    }
}