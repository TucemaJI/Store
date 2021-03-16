using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public IQueryable<Order> GetFilteredList(OrderFilter filter)
        {
            var orders = _dbSet.Include(item => item.OrderItems).ThenInclude(item=>item.PrintingEdition)
                .Where(o => o.UserId == filter.UserId)
                .Where(o => filter.Status == StatusType.None || o.Status == filter.Status);
            
            return orders;
        }
    }
}
