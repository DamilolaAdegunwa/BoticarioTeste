using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.Domain.Interfaces.Repositories
{
    public interface IRepository<T, Key> : IDisposable where T : class
    {
        List<T> List(Func<T, bool> filter);
        //Page<T> List(Func<T, bool> filter, PaginationOptions paginationOptions);
        int Count(Func<T, bool> include = null);
        T Find(Key key);
        Task<T> FindAsync(Key key);
        Task<T> UpdateAsync(Key key, T entity);
        T Insert(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        T Update(Key key, T entity);
        T Delete(Key key);
        void Delete(T entity);
        void Clean();

        Task<T> InsertAsync(T entity);
        Task<List<T>> ListAsync();
        Task<T> DeleteAsync(Key key);
        Task SaveAsync(Key key, T entity);
    }
}
