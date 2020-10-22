using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class //todo dbSet instead of context
    {
        private readonly ApplicationContext _applicationContext;
        protected readonly DbSet<T> _dbSet;
        public BaseEFRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = _applicationContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity: entity);
        }
        public async Task DeleteAsync(long item)
        {
            var element = await _applicationContext.Set<T>().FindAsync(item);
            _applicationContext.Set<T>().Remove(element);
        }
        public async Task<T> GetItemAsync(long id)
        {
            return await _applicationContext.Set<T>().FindAsync(id);
        }
        public Task<List<T>> GetListAsync()
        {
            return _applicationContext.Set<T>().ToListAsync();
        }
        public Task SaveAsync()
        {
            return _applicationContext.SaveChangesAsync();
        }
        public void Update(T item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

    }
}
