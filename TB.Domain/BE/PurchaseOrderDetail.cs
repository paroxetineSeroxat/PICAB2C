using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("PurchaseOrderDetail")]
    public class PurchaseOrderDetail
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal GrandTotal { get; set; }
        public int PurchaseOrderId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }

    }
}
