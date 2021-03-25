using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Extentions;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly ApplicationContext _applicationContext;
        public BaseEFRepository(ApplicationContext applicationContext)
        {
            _dbSet = applicationContext.Set<T>();
            _applicationContext = applicationContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity: entity);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(long item)
        {
            var element = _dbSet.FindAsync(item);
            _dbSet.Remove(await element);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<T> GetItemAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            _dbSet.Update(item).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
        }
        public Task<List<T>> GetSortedListAsync(BaseFilter filter, IQueryable<T> query)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByString))
            {
                filter.OrderByString = RepositoryOptions.DEFAULT_SEARCH;
            }
            var sortedT = query.OrderBy(filter.OrderByString, filter.IsDescending)
                .ToSortedListAsync(filter.EntityParameters.CurrentPage, filter.EntityParameters.ItemsPerPage);

            return sortedT;
        }

    }
}
