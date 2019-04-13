using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using TB.Domain.BE;

namespace TB.Web.Authentication
{

    public class CustomIdentity : IIdentity
    {

        /// <summary>
        /// An identity object represents the user on whose behalf the code is running.
        /// </summary>
        //[Serializable]
        public IIdentity Identity { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public bool IsLockedOut { get; set; }

        //public List<Module> modules { get; set; }

        #region Implementation of IIdentity

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name
        {
            get { return Identity.Name; }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        #endregion

        #region Constructor

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;


            var appMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (appMembershipUser != null)
            {
                Id = appMembershipUser.Id; 
                FirstName = appMembershipUser.FirstName;
                LastName = appMembershipUser.LastName;
                Email = appMembershipUser.Email;
                UserRoleId = appMembershipUser.UserRoleId;
                UserRoleName = appMembershipUser.UserRoleName;
                IsLockedOut = appMembershipUser.IsLockedOut;
                //modules = appMembershipUser.modules;
            }
        }

        public CustomIdentity(string documentNumber, string documentType)
        {
            //Identity = identity;
            var appMembershipUser = (CustomMembershipUser)CustomMembershipProvider.GetUser(documentNumber, true, true);
            if (appMembershipUser != null)
            {
                Id = appMembershipUser.Id;
                FirstName = appMembershipUser.FirstName;
                LastName = appMembershipUser.LastName;
                Email = appMembershipUser.Email;
                UserRoleId = appMembershipUser.UserRoleId;
                UserRoleName = appMembershipUser.UserRoleName;
                IsLockedOut = appMembershipUser.IsLockedOut;
                //modules = appMembershipUser.modules;
            }
        }

        #endregion
    }
}