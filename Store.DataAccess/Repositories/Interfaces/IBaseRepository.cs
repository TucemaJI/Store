using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetItemAsync(long id);
        void CreateAsync(T item);
        void DeleteAsync(long id);
        void Update(T item);
        void SaveAsync();
    }
}
