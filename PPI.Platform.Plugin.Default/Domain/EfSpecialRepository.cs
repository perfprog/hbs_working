using System.Linq;
using System.Data.Entity;

namespace PPI.Platform.Plugin.Default.Domain
{
    using PPI.Platform.Core.Domain;    
    public class EfSpecialRepository<T> : ISpecialRepository<T> where T : class
    {

        private DbContext Context;

        public EfSpecialRepository(DbContext context)
        {
            Context = context;
        }        
        public IQueryable<T> RunQuery(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(query, parameters).ToList<T>().AsQueryable();
        }
    }
}
