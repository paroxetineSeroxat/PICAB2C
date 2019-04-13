using TB.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("AppUser")]
    public class AppUser : BaseModel
    {
        public AppUser()
        {

        }
   
        public override int Id { get; set; }
        [Column("Username", TypeName = "nvarchar")]
        public override string Name { get; set; }


        [Column("Name", TypeName = "nvarchar")]
        public string   Names               { get; set; }
        public string   Surname             { get; set; }
        public string   DocumentNumber      { get; set; }
        public string   DocumentType        { get; set; }
        public string   MobileNumber        { get; set; }
        public string   Phone               { get; set; }
        public string   Address1            { get; set; }
        public string   Address2            { get; set; }
        public char     Genre               { get; set; }
        public int      Category            { get; set; }
        public string   Email               { get; set; }
        public int      CityId              { get; set; }
        public string Password              { get; set; }

        public virtual City City { get; set; }

    }
}
