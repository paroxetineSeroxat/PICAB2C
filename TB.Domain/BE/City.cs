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
    [Table("City")]
    public class City : BaseModel
    {
        public City()
        {

        }

        public override int Id { get; set; }
        public override string Name { get; set; }
        public string Code { get; set; }


    }
}