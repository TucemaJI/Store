using Store.DataAccess.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IDapperRepository<T> : IBaseRepository<T> where T : class
    {
        public List<T> GetSortedList(BaseFilter filter, IEnumerable<T> query);
    }
}
