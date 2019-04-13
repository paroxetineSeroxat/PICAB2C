using TB.Domain.BE;
using TB.Domain.BE.LDAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TB.Business.Interfaces
{
    public interface IAppUser
    {
        List<AppUser> GetAllAppUser();
        AppUser FindByIdAppUser(int id);
        int Add(AppUser appUser);
        //Los clientes podrán ingresar al sitio e inscribirse ingresando sus datos principales , como modificarlos
        string Update(AppUser appUser);
        string Authenticated(AppUser login);
        AppUser FindByName(string appUserName);
        List<AppUser> GetAllAppUser(Expression<Func<AppUser, bool>> Predicate);

        List<LdapUser> GetAllUsersDirectory();
        List<LdapUser> FindUserDirectory(LdapUser user);
    }
}
