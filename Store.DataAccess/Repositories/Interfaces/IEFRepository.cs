using Store.DataAccess.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IEFRepository<T> : IBaseRepository<T> where T : class
    {
        Task<List<T>> GetSortedListAsync(BaseFilter filter, IQueryable<T> query);
    }
}
