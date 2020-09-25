using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _db;
        public BaseEFRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async void CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity: entity);
        }
        public async void DeleteAsync(long item)
        {
            var element = await _db.Set<T>().FindAsync(item);
            _db.Set<T>().Remove(element);
        }
        public async Task<T> GetItemAsync(long id)
        {
            return await _db.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }
        public async void SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
