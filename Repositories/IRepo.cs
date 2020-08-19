using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAPI.Repositories
{
    public interface IRepo<T> {
        Task<IEnumerable<T>> get(int pageNumber = 1, int pageSize = 10);
        Task<T> getById(long id);
        Task<T> add(T entity);
        Task<T> update(long id, T entity);
        Task<T> delete(long id);
    }
}