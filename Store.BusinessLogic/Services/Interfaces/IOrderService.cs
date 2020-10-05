using Store.BusinessLogic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderModel> GetOrderModelAsync(long id);
        public Task CreateOrderAsync(OrderModel model);

        public Task<IEnumerable<OrderModel>> GetOrderModelsAsync();
        public Task DeleteOrderAsync(long id);
        public void UpdateOrder(OrderModel orderModel);
    }
}
