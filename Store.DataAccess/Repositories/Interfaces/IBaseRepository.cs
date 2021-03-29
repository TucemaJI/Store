using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetItemAsync(long id);
        Task CreateAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
        Task<List<T>> GetSortedListAsync(BaseFilter filter, IQueryable<T> query);
    }
}
