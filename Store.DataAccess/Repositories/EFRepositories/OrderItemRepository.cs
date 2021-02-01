using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderItemRepository : BaseEFRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task CreateOrderItemsAsync(IEnumerable<OrderItem> list)
        {
            await _dbSet.AddRangeAsync(list);
        }
    }
}
