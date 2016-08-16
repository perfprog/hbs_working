using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace PPI.Platform.Core.Domain
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
        Task<T> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);        
        T Single(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        void Add(IEnumerable<T> entity);
        void Delete(IEnumerable<T> entity);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

    }
}
