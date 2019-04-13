using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("Transport")]
    public class Transport
    {
        
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public int Type { get; set; }
        public int CompanyId { get; set; }

        public virtual TransportCompany Company { get; set; }
        [ForeignKey("Origin")]
        public virtual City OriginCity { get; set; }
        [ForeignKey("Destination")]
        public virtual City DestinationCity { get; set; }

    }
}
