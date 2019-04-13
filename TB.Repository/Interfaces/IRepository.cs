using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TB.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> Predicate);
        T FindById(int id);
        T FindByName(string id);
        int Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
