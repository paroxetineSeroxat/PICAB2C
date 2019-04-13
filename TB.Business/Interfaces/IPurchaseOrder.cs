using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.BE;

namespace TB.Business.Interfaces
{
    public interface IPurchaseOrder
    {
        
        int CreatePurchaseOrder(Domain.BE.PurchaseOrder purchaseOrder);

        //El sistema permitirá la cancelación y eliminación de órdenes de pedido que maneja TouresBalón/KS
        bool UpdatePurchaseOrder(Domain.BE.PurchaseOrder purchaseOrder);
        //Buscar órdenes por número 
        Domain.BE.PurchaseOrder GetPurchaseOrderbyNumber(int id);
        //Buscar órdenes que contengan un producto específico dando su número 
        List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyProductId(int id, DateTime starDate, DateTime endDate);

        //Listará todas sus órdenes que se encuentren abiertas y se podrá ver el detalle de cada una
        //Número de órdenes cerradas y total facturado por mes 
        //Ranking de las órdenes que llevan más tiempo abiertas, es decir, las órdenes de pedido ordenadas
        //desde la que más tiempo lleva abierta
        List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyStatus(int status, DateTime starDate, DateTime endDate);
        
        //Cuando el usuario selecciona una orden para ver su detalle, el sistema deberá consultar en los
        //proveedores de mensajería respectiva para actualizar el estado de la orden.
        PurchaseOrderDetail GetPurchaseOrderDetailById(int id);

        List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyCustomerId(int customerId);

    }
}
