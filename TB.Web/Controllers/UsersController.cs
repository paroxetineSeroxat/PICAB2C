using TB.Business.Admin;
using TB.Business.Interfaces;
using TB.Domain.BE;
using TB.Domain.BE.LDAP;
using TB.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TB.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            try
            {
                CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                IAppUser appUserBO = new AppUserBO();
                List<AppUser> listAppUser = appUserBO.GetAllAppUser(x => x.Name != "lylAdmin");

                

                return View(listAppUser);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            
        }

        [HttpPost]
        public string Update(AppUser appUser)
        {
            CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
            try
            {
                IAppUser appUserBO = new AppUserBO(customIdentity.Name);
                string message = appUserBO.Update(appUser);
                return message;
            }
            catch (Exception ex)
            {
    
                
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult SearchUser(LdapUser user)
        {
            CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
            try
            {
                List<LdapUser> listUser = new List<LdapUser>();
                List<LdapUser> listUserFilter = new List<LdapUser>();
                if (ConfigurationManager.AppSettings["LdapMockup"].ToLower().Equals("true"))
                {
                    //Ldap ldap = new Ldap();

                    //listUser = ldap.GetList();

                    //if (user.UserName != "" && user.UserName != null)
                    //{
                    //    listUserFilter = listUser.FindAll(x => x.UserName.Trim().ToLower().Contains(user.UserName.Trim().ToLower()));
                    //}

                    //if (user.Name != "" && user.Name != null)
                    //{
                    //    listUserFilter = listUser.FindAll(x => x.Name.Trim().ToLower().Contains(user.Name.Trim().ToLower()));
                    //}

                    //if (user.Email != "" && user.Email != null)
                    //{
                    //    listUserFilter = listUser.FindAll(x => x.Email.Trim().ToLower().Contains(user.Email.Trim().ToLower()));
                    //}
                    //listUser = ldap.GetList().FindAll(x => x.UserName.Trim().ToLower().Contains(user.UserName.Trim().ToLower()) ||
                    //    x.Name.Trim().ToLower().Contains(user.Name.Trim().ToLower()) ||
                    //    x.Email.Trim().ToLower().Contains(user.Email.Trim().ToLower()));

                    return Json(listUserFilter);
                }
                else
                {
                    IAppUser userBO = new AppUserBO(customIdentity.Name);
                    listUser = userBO.FindUserDirectory(user);
                    return Json(listUser);
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public string Add(AppUser appUser)
        {
            CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
            try
            {
                IAppUser appUserBO = new AppUserBO(customIdentity.Name);
                int id = appUserBO.Add(appUser);
                if(id > 0)
                    return "Usuario agregado exitosamente";
                return "Error Agregando usuario, intente nuevamente.";
            }
            catch (Exception ex)
            {
              
                throw new Exception(ex.Message);
            }
        }


    }
}
