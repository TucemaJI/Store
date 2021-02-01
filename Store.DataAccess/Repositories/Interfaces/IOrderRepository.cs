using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<PagedList<Order>> GetFilterSortedPagedListAsync(OrderFilter filter);
    }
}
