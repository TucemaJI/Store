using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderItemRepository : BaseEFRepository<OrderItem>, IOrderItemRepository<OrderItem>
    {
        public OrderItemRepository(ApplicationContext db) : base(db) { }

    }
}
