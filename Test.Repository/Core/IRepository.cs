using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test.Repository
{
    public interface IRepository<T> where T : class
    {

        T GetById(int id);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        int Add(T entity);
        int Update(T entity);
        int Remove(T entity);

        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        int CountAll();
        int CountWhere(Expression<Func<T, bool>> predicate);

    }
}