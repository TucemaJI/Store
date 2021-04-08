﻿using Microsoft.EntityFrameworkCore;
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
    public abstract class BaseEFRepository<T> : IEFRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly ApplicationContext _applicationContext;
        public BaseEFRepository(ApplicationContext applicationContext)
        {
            _dbSet = applicationContext.Set<T>();
            _applicationContext = applicationContext;
        }

        protected async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity: entity);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _applicationContext.SaveChangesAsync();
        }
        public async virtual Task<T> GetItemAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(T item)
        {
            _applicationContext.Update(item).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
        }
        public Task<List<T>> GetSortedListAsync(BaseFilter filter, IQueryable<T> query)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByField))
            {
                filter.OrderByField = RepositoryConsts.DEFAULT_SEARCH;
            }
            var sortedT = query.OrderBy(filter.OrderByField, filter.IsDescending)
                .ToSortedListAsync(filter.PageOptions.CurrentPage, filter.PageOptions.ItemsPerPage);

            return sortedT;
        }

    }
}
