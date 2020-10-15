using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        public Task CreateOrderItemsAsync(IEnumerable<OrderItem> list);
    }
}
