using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderModel> GetOrderModelAsync(long id);
        public Task<long> CreateOrderAsync(OrderModel model);
        public Task<PageModel<OrderModel>> GetOrderModelListAsync(OrderFilter filter);
        public Task DeleteOrderAsync(long id);
        public Task<bool> Pay(OrderPayModel model);

    }
}
