using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Base;

namespace TB.Repository.Repositories
{
    public class TransportRepository : BaseRepository<Transport>
    {
        public TransportRepository(TBContext context) : base(context)
        {
        }

        public override int Add(Transport entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Transport entity)
        {
            throw new NotImplementedException();
        }

        public override Transport FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    Transport query = ((TBContext)context).Transport.Include(x => x.DestinationCity).Include(x => x.OriginCity).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override Transport FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).Transport.Any(u => u.Description == name);
                    if (exist)
                    {
                        Transport query = ((TBContext)context).Transport.Include(x => x.DestinationCity).Include(x => x.OriginCity).First(u => u.Description == name);
                        return query;
                    }
                    else
                        return null;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<Transport> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Transport>().Include(x => x.DestinationCity).Include(x => x.OriginCity).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<Transport> GetAll(Expression<Func<Transport, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Transport>().Include(x => x.DestinationCity).Include(x => x.OriginCity).Where(Predicate).ToList();
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

        public override void Update(Transport entity)
        {
            throw new NotImplementedException();
        }
    }
}
