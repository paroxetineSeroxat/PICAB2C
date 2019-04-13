using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Business.Interfaces;
using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Repositories;

namespace TB.Business.PurchaseOrder
{
    public class PurchaseOrderBO : IPurchaseOrder
    {
        TBContext context = new TBContext();
        private string currentUser = string.Empty;

        public PurchaseOrderBO(string currentUser)
        {
            this.currentUser = currentUser;
        }

        public int CreatePurchaseOrder(Domain.BE.PurchaseOrder purchaseOrder)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);
                repo.Add(purchaseOrder);
                return purchaseOrder.Id;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Domain.BE.PurchaseOrder GetPurchaseOrderbyNumber(int id)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);
                return repo.FindById(id);
                

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyProductId(int id, DateTime starDate, DateTime endDate)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);

                return repo.GetPurchaseOrderbyProductId(id, starDate, endDate);               
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyStatus(int status, DateTime starDate, DateTime endDate)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);

                return repo.GetAll(x => x.Status == status && (x.CreationDate >= starDate && x.CreationDate <= endDate));

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            try
            {
                PurchaseOrderDetailRepository repo = new PurchaseOrderDetailRepository(context);

                return repo.FindById(id);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool UpdatePurchaseOrder(Domain.BE.PurchaseOrder purchaseOrder)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);

                repo.Update(purchaseOrder);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Domain.BE.PurchaseOrder> GetPurchaseOrderbyCustomerId(int customerId)
        {
            try
            {
                PurchaseOrderRepository repo = new PurchaseOrderRepository(context);

                return repo.FindByCustomerId(customerId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
