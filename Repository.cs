using CustomerCrud.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerCrud.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> dbSet;
        internal CustomersEntities dbContext;

        public Repository(CustomersEntities dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return dbSet.AsQueryable().Where(predicate);
        }

        public T GetSingle(int id)
        {
            return dbSet.Find(id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return dbSet.AsQueryable().Where(predicate).FirstOrDefault();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}