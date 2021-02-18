using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public Task<List<Order>> GetFilterSortedListAsync(OrderFilter filter)
        {
            var orders = _dbSet.Include(item => item.OrderItems)
                .Where(o => o.PaymentId == filter.Payment)
                .Where(o => o.Status == filter.Status)
                .Where(o => o.OrderItems.Any(oi => EF.Functions.Like(oi.Currency, $"%{filter.Currency}%")));
            var sortedOrders = GetSortedListAsync(filter: filter, ts: orders);
            return sortedOrders;
        }
    }
}
