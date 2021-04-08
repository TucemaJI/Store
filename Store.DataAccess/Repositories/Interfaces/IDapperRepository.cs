using Store.DataAccess.Models.Filters;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IDapperRepository<T> : IBaseRepository<T> where T : class
    {
        public List<T> GetSortedList(BaseFilter filter, IEnumerable<T> query);
    }
}
