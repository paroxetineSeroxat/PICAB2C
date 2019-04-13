using TB.Business.Interfaces;
using TB.Business.Util.Cryptography;
using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Repositories;
using TB.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.BE.LDAP;

namespace TB.Business.Admin
{
    public class AppUserBO : IAppUser
    {
        TBContext context = new TBContext();
        private string currentUser = string.Empty;
        public AppUserBO()
        {

        }
        public AppUserBO(string currentUser)
        {
            this.currentUser = currentUser;
        }

        public int Add(AppUser appUser)
        {
            try
            {
                AppUserRepository appUserRepository = new AppUserRepository(context);
                return appUserRepository.Add(appUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Authenticated(AppUser login)
        {
            try
            {
                AppUserRepository appUserRepository = new AppUserRepository(context);
                AppUser findAppUser = appUserRepository.FindByName(login.Name);

                if (findAppUser != null)
                {
                    //if (login.Name != "TouresBalon")
                    //{
                        
                    //        //if (findAppUser.Active)
                    //        //{
                    //            string path = "LDAP://DC=" + System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectory"] + ",DC=" + System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectoryExtension"];
                    //            string filterAttribute = "";

                    //            string domainAndUsername = System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectory"] + "." + System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectoryExtension"] + @"\" + login.Name;
                    //            DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, login.Password);

                    //            try
                    //            {
                    //                object obj = entry.NativeObject;
                    //                DirectorySearcher search = new DirectorySearcher(entry);
                    //                search.Filter = "(SAMAccountName=" + login.Name + ")";
                    //                search.PropertiesToLoad.Add("cn");
                    //                SearchResult result = search.FindOne();

                    //                if (null == result)
                    //                {
                    //                    return LoginStatus.WRONG_PASSWORD;
                    //                }
                    //                path = result.Path;
                    //                filterAttribute = (String)result.Properties["cn"][0];
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                return LoginStatus.WRONG_PASSWORD;
                    //            }
                    //            return LoginStatus.OK;
                    //        //}
                    //        //else
                    //        //{
                    //        //    return LoginStatus.USER_INACTIVE;
                    //        //}
                       
                    //}
                    //else
                    //{

                        EncryptionFactory factory = new EncryptionFactory();
                        IEncryptionFactory encriptor = factory.GetProvider(EncryptionProvider.TripeDes);
                        encriptor.Key = ConfigurationManager.AppSettings["encriptorKey"];
                        encriptor.IV = ConfigurationManager.AppSettings["encriptorIV"];
                        string encriptPassword = encriptor.Encrypt(login.Password);
                        if (encriptPassword == findAppUser.Password)
                        {
                            return LoginStatus.OK;
                        }
                        else
                        {
                            return LoginStatus.WRONG_PASSWORD;
                        }
                    //}
                }
                else
                {
                    return LoginStatus.APPUSER_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                throw;
            }
        }

        public AppUser FindByIdAppUser(int id)
        {
            try
            {
                AppUserRepository repository = new AppUserRepository(context);
                AppUser objectAppUser = repository.FindById(id);
                return objectAppUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AppUser FindByName(string appUserName)
        {
            try
            {
                AppUserRepository repository = new AppUserRepository(context);
                AppUser objectAppUser = repository.FindByName(appUserName);
                return objectAppUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppUser> GetAllAppUser()
        {
            try
            {
                AppUserRepository repository = new AppUserRepository(context);
                List<AppUser> list = repository.GetAll();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AppUser> GetAllAppUser(Expression<Func<AppUser, bool>> Predicate)
        {
            try
            {
                AppUserRepository repository = new AppUserRepository(context);
                List<AppUser> list = repository.GetAll(Predicate);

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Update(AppUser appUser)
        {
            throw new NotImplementedException();
        }


        //LDAP

        public List<LdapUser> GetAllUsersDirectory()
        {
            try
            {
                List<LdapUser> listUsersActiveDirectory = new List<LdapUser>();
                string DomainPath = "LDAP://DC=" + System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectory"] + ",DC=" + System.Configuration.ConfigurationManager.AppSettings["domainActiveDirectoryExtension"];

                using (DirectoryEntry directoryEntry = new DirectoryEntry(DomainPath))
                {
                    DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
                    directorySearcher.PageSize = 10;
                    directorySearcher.Filter = "(&(objectClass=user)(objectCategory=Person)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
                    directorySearcher.PropertiesToLoad.Add("samaccountname");
                    directorySearcher.PropertiesToLoad.Add("title");
                    directorySearcher.PropertiesToLoad.Add("mail");
                    directorySearcher.PropertiesToLoad.Add("usergroup");
                    directorySearcher.PropertiesToLoad.Add("company");
                    directorySearcher.PropertiesToLoad.Add("department");
                    directorySearcher.PropertiesToLoad.Add("telephoneNumber");
                    directorySearcher.PropertiesToLoad.Add("mobile");
                    directorySearcher.PropertiesToLoad.Add("displayname");
                    SearchResult result;
                    LdapUser itemUser;

                    SearchResultCollection iResult = directorySearcher.FindAll();

                    
                    if (iResult != null)
                    {
                        AppUserRepository appUserRepository = new AppUserRepository(context);
                        List<AppUser> listAppUser = appUserRepository.GetAll();
                        //listAppUser.ForEach(x => x.Companys.ToList().ForEach(c => c.AppUsers = null));
                        for (int i = 0; i < iResult.Count; i++)
                        {
                            result = iResult[i];
                            if (result.Properties.Contains("samaccountname"))
                            {
                                itemUser = new LdapUser();

                                itemUser.UserName = (String)result.Properties["samaccountname"][0];

                                if (result.Properties.Contains("displayname"))
                                {
                                    itemUser.Name = (String)result.Properties["displayname"][0];
                                }
                                else
                                {
                                    itemUser.Name = string.Empty;
                                }

                                if (result.Properties.Contains("mail"))
                                {
                                    itemUser.Email = (String)result.Properties["mail"][0];
                                }
                                else
                                {
                                    itemUser.Email = string.Empty;
                                }

                                if (result.Properties.Contains("company"))
                                {
                                    itemUser.Company = (String)result.Properties["company"][0];
                                }

                                if (result.Properties.Contains("title"))
                                {
                                    itemUser.JobTitle = (String)result.Properties["title"][0];
                                }

                                if (result.Properties.Contains("department"))
                                {
                                    itemUser.Deparment = (String)result.Properties["department"][0];
                                }

                                if (result.Properties.Contains("telephoneNumber"))
                                {
                                    itemUser.Phone = (String)result.Properties["telephoneNumber"][0];
                                }

                                if (result.Properties.Contains("mobile"))
                                {
                                    itemUser.Mobile = (String)result.Properties["mobile"][0];
                                }

                                AppUser auxAppUser = listAppUser.Find(x => x.Name == itemUser.UserName);

                                if (auxAppUser != null)
                                {
                                    itemUser.Exists = true;
                                    //if (auxAppUser.Profile != null)
                                    //{
                                    //    itemUser.Profile = auxAppUser.Profile;
                                    //}

                                    //itemUser.Companys = auxAppUser.Companys;

                                    //itemUser.Active = auxAppUser.Active;
                                }

                                listUsersActiveDirectory.Add(itemUser);
                            }
                        }
                    }

                    directorySearcher.Dispose();
                    directoryEntry.Dispose();

                    return listUsersActiveDirectory;
                }
            }
            catch (Exception ex)
            {

                string inner = string.Empty;
                if (ex.InnerException != null)
                {
                    inner = " inner: " + ex.InnerException.Message;
                }

               
                throw new Exception(ex.Message);

               
            }
        }

        public List<LdapUser> FindUserDirectory(LdapUser user)
        {
            try
            {
                
                List<LdapUser> listUser = GetAllUsersDirectory();
                List<LdapUser> returnList = new List<LdapUser>();

                AppUserRepository appUserRepository = new AppUserRepository(context);
                List<AppUser> listAppUser = appUserRepository.GetAll();

                if (user.UserName != "" && user.UserName != null)
                {
                    listUser = listUser.FindAll(x => x.UserName.Trim().ToLower().Contains(user.UserName.Trim().ToLower()));
                }

                if (user.Name != "" && user.Name != null)
                {
                    listUser = listUser.FindAll(x => x.Name.Trim().ToLower().Contains(user.Name.Trim().ToLower()));
                }

                if (user.Email != "" && user.Email != null)
                {
                    listUser = listUser.FindAll(x => x.Email.Trim().ToLower().Contains(user.Email.Trim().ToLower()));
                }


                foreach (LdapUser item in listUser)
                {
                    var apUserAux = listAppUser.Find(x => x.Name.Trim().ToLower() == item.UserName.Trim().ToLower());
                    if (apUserAux == null)
                    {
                        returnList.Add(item);
                    }
                }

                return returnList;
            }
            catch (Exception ex)
            {

                string inner = string.Empty;
                if (ex.InnerException != null)
                {
                    inner = " inner: " + ex.InnerException.Message;
                }

                
                throw new Exception(ex.Message);
            }
        }

        //END LDAP
    }
}
