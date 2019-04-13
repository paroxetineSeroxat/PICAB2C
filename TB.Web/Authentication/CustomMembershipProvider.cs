using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using System.Web.Caching;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using TB.Domain.BE;
using TB.Business.Admin;
using TB.Business.Interfaces;
using TB.Domain.Constants;

namespace TB.Web.Authentication
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private static int _cacheTimeoutInMinutes = 30;

        /// <summary>
        /// Initialize values from web.config.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to validate. </param><param name="password">The password for the specified user. </param>
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            else
            {
                AppUser login = new AppUser();

                login.Password = password;
                login.Name = username;

                IAppUser appUserBO = new AppUserBO();
                string loginStatus = appUserBO.Authenticated(login);
                //string loginStatus = LoginStatus.OK;
                if (loginStatus.Equals(LoginStatus.OK))
                {
                    var userRoles = new string[] { };

                    var cacheKey = string.Format("UserRoles_{0}", login.Name);

                    int _cacheTimeoutInMinutes = 30;

                    HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
                    return true;
                }
                return false;
            }
        }

        public static bool ValidateUser(string username, string password, bool status)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            else
            {
                AppUser login = new AppUser();

                login.Password = password;
                login.Name = username;

                IAppUser appUserBO = new AppUserBO();
                string loginStatus = appUserBO.Authenticated(login);
                //string loginStatus = LoginStatus.OK;
                if (loginStatus.Equals(LoginStatus.OK))
                {
                    var userRoles = new string[] { };

                    var cacheKey = string.Format("UserRoles_{0}", login.Name);

                    int _cacheTimeoutInMinutes = 30;

                    HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
                    return true;
                }
                return false;
            }
        }

        public static string ValidateUserActiveDirectory(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return LoginStatus.LOGIN_ERROR;
            else
            {
                AppUser login = new AppUser();

                login.Password = password;
                login.Name = username;

                IAppUser appUserBO = new AppUserBO();
                string loginStatus = appUserBO.Authenticated(login);
                //string loginStatus = LoginStatus.OK;
                string statusReturn = loginStatus;

                switch (loginStatus)
                {
                    case LoginStatus.OK:
                        var userRoles = new string[] { };
                        var cacheKey = string.Format("UserRoles_{0}", login.Name);
                        int _cacheTimeoutInMinutes = 30;
                        userRoles = new[] { "Administrator" };
                        HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
                        break;
                }

                return statusReturn;
            }
        }

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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">The name of the user to get information for. </param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var cacheKey = string.Format("UserData_{0}", username);

            if (HttpRuntime.Cache[cacheKey] != null)
                return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

            AppUser user = new AppUser();
            IAppUser appUserBO = new AppUserBO();
            user = appUserBO.FindByName(username);

            if (user != null)
            {
                //user.Profile.PMG.ToList().ForEach(x => x.Module.PMG = null);
                //user.Profile.PMG.ToList().ForEach(x => x.Module.Grant = x.Grant);
                //foreach (var item in user.Profile.PMG)
                //{
                //    item.Profile = null;
                //}

                var membershipUser = new CustomMembershipUser(user);

                HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);

                return membershipUser;
            }
            else
            {
                return null;
            }

        }

        public static MembershipUser GetUser(AppUser appUser)
        {
            var cacheKey = string.Format("UserData_{0}", appUser.Name); //username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

            AppUser user = new AppUser();
            IAppUser appUserBO = new AppUserBO();
            user = appUserBO.FindByName(appUser.Name);


            if (user == null)
            {
                user = new AppUser();
            }
            else
            {
                //user.Profile.PMG.ToList().ForEach(x => x.Module.PMG = null);
                //user.Profile.PMG.ToList().ForEach(x => x.Module.Grant = x.Grant);
                //foreach (var item in user.Profile.PMG)
                //{
                //    item.Profile = null;
                //}
            }

            var membershipUser = new CustomMembershipUser(user);

            //Store in cache
            HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);

            return membershipUser;
            //}
        }

        public static MembershipUser GetUser(string userName, bool userIsOnline, bool status)
        {
            var cacheKey = string.Format("UserData_{0}", userName);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

            AppUser user = new AppUser();
            IAppUser appUserBO = new AppUserBO();
            user = appUserBO.FindByName(userName);


            if (user != null)
            {
                //user.Profile.PMG.ToList().ForEach(x => x.Module.PMG = null);
                //user.Profile.PMG.ToList().ForEach(x => x.Module.Grant = x.Grant);
                //foreach (var item in user.Profile.PMG)
                //{
                //    item.Profile = null;
                //}

                var membershipUser = new CustomMembershipUser(user);

                HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), System.Web.Caching.Cache.NoSlidingExpiration);

                return membershipUser;
            }
            return null;
        }
        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">The name of the user to get information for. </param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return new MembershipPasswordFormat(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}