using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Linq;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public IQueryable<Order> GetFilteredQuery(OrderFilter filter);
    }
}
