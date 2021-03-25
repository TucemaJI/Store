using Microsoft.EntityFrameworkCore;
using Store.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Extentions
{
    public static class ToSortedListExtention
    {
        public static async Task<List<TSource>> ToSortedListAsync<TSource>(this IOrderedQueryable<TSource> source, int pageNumber, int pageSize)
        {
            var items = await source.Skip((pageNumber - PagedListOptions.CORRECTING_PAGE_NUMBER) * pageSize).Take(pageSize).ToListAsync();

            return items;
        }
    }
}
