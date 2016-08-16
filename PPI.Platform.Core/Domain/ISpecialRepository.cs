using System.Linq;

namespace PPI.Platform.Core.Domain
{
    public interface ISpecialRepository<T> where T : class
    {
        IQueryable<T> RunQuery(string query, params object[] parameters);
    }
}
