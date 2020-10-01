using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;
        public BaseEFRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async void CreateAsync(T entity)
        {
            await _applicationContext.Set<T>().AddAsync(entity: entity);
        }
        public async void DeleteAsync(long item)
        {
            var element = await _applicationContext.Set<T>().FindAsync(item);
            _applicationContext.Set<T>().Remove(element);
        }
        public async Task<T> GetItemAsync(long id)
        {
            return await _applicationContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _applicationContext.Set<T>().ToListAsync();
        }
        public async void SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
        public void Update(T item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }
    }
}
