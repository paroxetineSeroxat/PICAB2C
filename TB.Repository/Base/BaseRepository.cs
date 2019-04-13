using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TB.Repository.Base
{
    public abstract class BaseRepository<T> : Interfaces.IRepository<T> where T : class
    {

        protected DbContext context = null;
        private DbSet<T> entity = null;

        public BaseRepository(DbContext context)
        {
            this.context = context;

            this.entity = context.Set<T>();



        }

        public DbContext Context
        {
            get
            {
                return context;
            }
        }

        public abstract int Add(T entity);
     
        public abstract void Delete(T entity);
       
        public abstract T FindById(int id);
       
        public abstract T FindByName(string name);

        public abstract List<T> GetAll();
      
        public abstract List<T> GetAll(Expression<Func<T, bool>> Predicate);
      
        public abstract void Update(T entity);

        public abstract bool IsDisposed();


    }

}
