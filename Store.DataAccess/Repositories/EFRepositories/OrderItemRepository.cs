using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderItemRepository : BaseEFRepository<OrderItem>, IOrderItemRepository<OrderItem>
    {
        public OrderItemRepository(DbContextOptions<ApplicationContext> options) : base(options) { }
        public void Create(OrderItem item)
        {
            db.OrderItems.Add(item);
        }

        public void Delete(long item)
        {
            var oi = db.OrderItems.Find(item);
            if (oi != null) { db.OrderItems.Remove(oi); }
        }

        public IEnumerable<OrderItem> GetList()
        {
            return db.OrderItems;
        }

        public OrderItem GetItem(long id)
        {
            return db.OrderItems.Find(id);
        }

    }
}
