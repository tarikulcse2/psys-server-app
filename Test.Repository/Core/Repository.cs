using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.Entities.DBContext;

namespace Test.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {

        protected DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public T GetById(int id) => Context.Set<T>().Find(id);

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefault(predicate);

        public int Add(T entity)
        {
            Context.Set<T>().Add(entity);
            return Context.SaveChanges();
        }

        public int Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges();
        }

        public int Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public int CountAll() => Context.Set<T>().Count();

        public int CountWhere(Expression<Func<T, bool>> predicate) 
            => Context.Set<T>().Count(predicate);

    }
}