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
    public class HotelRepository : BaseRepository<Hotel>
    {
        public HotelRepository(TBContext context) : base(context)
        {
        }

        public override int Add(Hotel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Hotel entity)
        {
            throw new NotImplementedException();
        }

        public override Hotel FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    Hotel query = ((TBContext)context).Hotel.Include(x =>x.Rooms).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override Hotel FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).Hotel.Any(u => u.Name == name);
                    if (exist)
                    {
                        Hotel query = ((TBContext)context).Hotel.Include(x => x.Rooms).First(u => u.Name == name);
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

        public override List<Hotel> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Hotel>().Include(x => x.City).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<Hotel> GetAll(Expression<Func<Hotel, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Hotel>().Include(x => x.City).Where(Predicate).ToList();
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

        public override void Update(Hotel entity)
        {
            throw new NotImplementedException();
        }
    }
}
