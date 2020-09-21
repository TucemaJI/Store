using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository<Order>
    {
        public OrderRepository(DbContextOptions<ApplicationContext> option) : base(option) { }
        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(long id)
        {
            Order order = db.Orders.Find(id);
            if (order != null) { db.Orders.Remove(order); }
        }

        public Order GetItem(long id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetList()
        {
            return db.Orders;
        }
    }
}
