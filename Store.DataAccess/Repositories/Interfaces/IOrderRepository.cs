using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<(List<Order> orderList, long count)> GetOrderListAsync(OrderFilter filter);
    }
}
