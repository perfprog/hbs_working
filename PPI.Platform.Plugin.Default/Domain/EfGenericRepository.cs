using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PPI.Platform.Plugin.Default.Domain
{
    using PPI.Platform.Core.Domain;    
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {

        private DbContext Context;
        public EfGenericRepository(DbContext context)
        {
            Context = context;
        }


        
        public virtual IQueryable<T> AsQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }
        
        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return Context.Set<T>().SqlQuery(query, parameters);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
       

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
       

        public T Single(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Single(predicate);
        }
       

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().SingleOrDefault(predicate);
        }
       

        public T First(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }
       

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }
       

        public void Add(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRange(entity);
        }
       

        public void Delete(IEnumerable<T> entity)
        {
            Context.Set<T>().RemoveRange(entity);
        }
       

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
       

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
       

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
