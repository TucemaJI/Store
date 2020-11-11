using Microsoft.EntityFrameworkCore;
using Store.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Models
{
    public class PagedList<T>:List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > PagedListOptions.FIRST_PAGE;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static async Task<PagedList<T>> ToPagedListAsync(IOrderedQueryable<T> source, int pageNumber, int pageSize, bool isDescending)
        {
            var count = await source.CountAsync();
            if (isDescending)
            {
                source.Reverse();
            }
            var items = await source.Skip((pageNumber - PagedListOptions.CORRECTING_PAGE_NUMBER) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
