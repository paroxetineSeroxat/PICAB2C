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
    public class CityRepository : BaseRepository<City>
    {
        public CityRepository(TBContext context) : base(context)
        {
        }

        public override int Add(City entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(City entity)
        {
            throw new NotImplementedException();
        }

        public override City FindById(int id)
        {
            throw new NotImplementedException();
        }

        public override City FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public override List<City> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<City> GetAll(Expression<Func<City, bool>> Predicate)
        {
            throw new NotImplementedException();
        }

        public override void Update(City entity)
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

        
    }
}
