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
    public class RoomRepository : BaseRepository<Room>
    {
        public RoomRepository(TBContext context) : base(context)
        {
        }

        public override int Add(Room entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Room entity)
        {
            throw new NotImplementedException();
        }

        public override Room FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    Room query = ((TBContext)context).Room.First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override Room FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).Room.Any(u => u.Name == name);
                    if (exist)
                    {
                        Room query = ((TBContext)context).Room.First(u => u.Name == name);
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

        public override List<Room> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Room>().ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<Room> GetAll(Expression<Func<Room, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Room>().Where(Predicate).ToList();
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

        public override void Update(Room entity)
        {
            throw new NotImplementedException();
        }
    }
}
