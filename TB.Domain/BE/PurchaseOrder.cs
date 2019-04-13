using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        public int Status { get; set; }

        public virtual List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
