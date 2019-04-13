using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TB.Repository.Repositories
{
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>
    {
        public PurchaseOrderRepository(TBContext context) : base(context)
        {
        }

        public override int Add(PurchaseOrder entity)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    ((TBContext)context).PurchaseOrder.Add(entity);

                    return entity.Id;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void Delete(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }

        public override PurchaseOrder FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    PurchaseOrder query = ((TBContext)context).PurchaseOrder.Include(x => x.PurchaseOrderDetails).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override PurchaseOrder FindByName(string name)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    PurchaseOrder query = ((TBContext)context).PurchaseOrder.Include(x => x.PurchaseOrderDetails).First(u => u.Description == name);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<PurchaseOrder> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<PurchaseOrder>().ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<PurchaseOrder> GetAll(Expression<Func<PurchaseOrder, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<PurchaseOrder>().Where(Predicate).Include(x =>x.PurchaseOrderDetails.Select(y => y.Product)).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<PurchaseOrder> FindByCustomerId(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    List<PurchaseOrder> query = ((TBContext)context).PurchaseOrder.Include(x => x.PurchaseOrderDetails.Select(z => z.Product)).Where(u => u.CustomerId == id).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override bool IsDisposed()
        {
            var result = true;

            var typeDbContext = typeof(DbContext);
            var typeInternalContext = typeDbContext.Assembly.GetType("System.Data.Entity.Internal.InternalContext");

            var fi_InternalContext = typeDbContext.GetField("_internalContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pi_IsDisposed = typeInternalContext.GetProperty("IsDisposed");

            var ic = fi_InternalContext.GetValue(context);

            if (ic != null)
            {
                result = (bool)pi_IsDisposed.GetValue(ic);
            }

            return result;
        }

        public override void Update(PurchaseOrder entity)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<PurchaseOrder> GetPurchaseOrderbyProductId(int id, DateTime starDate, DateTime endDate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    
                    var list = context.Set<PurchaseOrder>().Where(x => x.CreationDate >= starDate && x.CreationDate <= endDate).Include(x => x.PurchaseOrderDetails.Select(y => y.ProductId == id)).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
