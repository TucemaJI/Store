﻿using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Extentions;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var element = await _dbSet.FindAsync(item);
            _dbSet.Remove(element);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<T> GetItemAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }
        public Task<List<T>> GetListAsync()// todo delete when filter works
        {
            return _dbSet.ToListAsync();
        }
        public async void UpdateAsync(T item)
        {
            _dbSet.Update(item).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
        }
        protected Task<PagedList<T>> GetSortedPagedList(BaseFilter filter, IQueryable<T> ts)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByString))
            {
                filter.OrderByString = "Id";
            }

            var sortedT = PagedList<T>.ToPagedListAsync(source: ts.OrderBy(filter.OrderByString),
                pageNumber: filter.EntityParameters.PageNumber,
                pageSize: filter.EntityParameters.PageSize,
                isDescending: filter.IsDescending);
            return sortedT;
        }

    }
}
