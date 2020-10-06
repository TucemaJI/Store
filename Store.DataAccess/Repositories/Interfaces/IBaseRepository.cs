using Store.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetListAsync();
        Task<T> GetItemAsync(long id);
        Task CreateAsync(T item);
        Task DeleteAsync(long id);
        void Update(T item);
        Task SaveAsync();
    }
}
