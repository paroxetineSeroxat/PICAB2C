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
    public class AppUserRepository : BaseRepository<AppUser>
    {
        public AppUserRepository(TBContext context) : base(context)
        {


        }

        public override int Add(AppUser entity)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    context.Set<AppUser>().Add(entity);
                    context.SaveChanges();
                    int id = entity.Id;
                    return id;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void Delete(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public override AppUser FindById(int id)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    AppUser query = ((TBContext)context).AppUser.First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override AppUser FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).AppUser.Any(u => u.Name == name);
                    if (exist)
                    {

                        AppUser query = ((TBContext)context).AppUser.Include(x => x.City).First(u => u.Name == name);
                        return query;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<AppUser> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<AppUser>().Include(x => x.City).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<AppUser> GetAll(Expression<Func<AppUser, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<AppUser>().Include(x => x.City).Where(Predicate).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void Update(AppUser entity)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {

                    var itemEntity = ((TBContext)context).AppUser.First(x => x.Id == entity.Id);
                    var item = context.Entry<AppUser>(itemEntity);
                    item.State = EntityState.Modified;

                    itemEntity.Names = entity.Names;
                    itemEntity.Surname = entity.Surname;
                    itemEntity.MobileNumber = entity.MobileNumber;
                    itemEntity.Phone = entity.Phone;
                    itemEntity.Address1 = entity.Address1;
                    itemEntity.Address2 = entity.Address2;
                    itemEntity.Genre = entity.Genre;
                    itemEntity.Category = entity.Category;
                    itemEntity.Email = entity.Email;
                    itemEntity.City = entity.City;
                    


                    context.SaveChanges();
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
    }
}
