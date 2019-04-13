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
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(TBContext context) : base(context)
        {
        }

        public override int Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public override Product FindById(int id)
        {
            
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    Product query = ((TBContext)context).Product.Include(x => x.Hotel).Include(x => x.Transport).Include(x => x.SportEvent).First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override Product FindByName(string name)
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
                        Product query = ((TBContext)context).Product.Include(x => x.Hotel).Include(x => x.Transport).Include(x => x.SportEvent).First(u => u.ProductName == name);
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

        public Product FindByCode(string code)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).Product.Any(u => u.ProductCode== code);
                    if (exist)
                    {
                        Product query = ((TBContext)context).Product.Include(x => x.Hotel).Include(x => x.Transport).Include(x => x.SportEvent).First(u => u.ProductCode == code);
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

        public override List<Product> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Product>().ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<Product> GetAll(Expression<Func<Product, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<Product>().Where(Predicate).ToList();
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

        public override void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
