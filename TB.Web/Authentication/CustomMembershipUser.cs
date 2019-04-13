
using TB.Domain.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TB.Web.Authentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        //public List<Module> modules { get; set; }

        #endregion

        public CustomMembershipUser(AppUser user)
            : base("CustomMembershipProvider", user.Name, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            FirstName = user.Name;
            LastName = user.Name;
            Id = user.Id;
            //modules = new List<Module>();
            //modules = user.Profile.PMG.Select(x => x.Module).OrderBy(a => a.Order).ToList();
        }
    }
}