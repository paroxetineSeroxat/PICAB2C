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
    public class RoomBookingRepository : BaseRepository<RoomBooking>
    {
        public RoomBookingRepository(TBContext context) : base(context)
        {
        }

        public override int Add(RoomBooking entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(RoomBooking entity)
        {
            throw new NotImplementedException();
        }

        public override RoomBooking FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    RoomBooking query = ((TBContext)context).RoomBooking.Include(x => x.Room).Include(x => x.Hotel).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override RoomBooking FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).RoomBooking.Any(u => u.Room.Name == name);
                    if (exist)
                    {
                        RoomBooking query = ((TBContext)context).RoomBooking.Include(x => x.Room).Include(x => x.Hotel).First(u => u.Room.Name == name);
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

        public override List<RoomBooking> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<RoomBooking>().ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<RoomBooking> GetAll(Expression<Func<RoomBooking, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<RoomBooking>().Include(x => x.Room).Include(x => x.Hotel).Where(Predicate).ToList();
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

        public override void Update(RoomBooking entity)
        {
            throw new NotImplementedException();
        }
    }
}
