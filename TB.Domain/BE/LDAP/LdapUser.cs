using TB.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE.LDAP
{
    public class LdapUser : BaseModel
    {
        public LdapUser()
        {
            // this.Companys = new HashSet<Company>();
        }

        public override int Id { get; set; }
        public override string Name { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Deparment { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public bool Active { get; set; }
        public bool Exists { get; set; }

    }
}

