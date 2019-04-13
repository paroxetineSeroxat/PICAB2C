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
    public class PurchaseOrderDetailRepository : BaseRepository<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailRepository(TBContext context) : base(context)
        {
        }

        public override int Add(PurchaseOrderDetail entity)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    ((TBContext)context).PurchaseOrderDetail.Add(entity);

                    return entity.Id;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void Delete(PurchaseOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public override PurchaseOrderDetail FindById(int id)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    PurchaseOrderDetail query = ((TBContext)context).PurchaseOrderDetail.Include(x => x.Product).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override PurchaseOrderDetail FindByName(string name)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    PurchaseOrderDetail query = ((TBContext)context).PurchaseOrderDetail.Include(x => x.Product).First(u => u.Description == name);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<PurchaseOrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<PurchaseOrderDetail> GetAll(Expression<Func<PurchaseOrderDetail, bool>> Predicate)
        {
            throw new NotImplementedException();
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

        public override void Update(PurchaseOrderDetail entity)
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
    }
}
