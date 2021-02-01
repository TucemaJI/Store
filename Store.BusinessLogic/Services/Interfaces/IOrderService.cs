﻿using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderModel> GetOrderModelAsync(long id);
        public Task CreateOrderAsync(OrderModel model);
        public Task<List<OrderModel>> GetOrderModelsAsync(OrderFilter filter);
        public Task DeleteOrderAsync(long id);
        public void UpdateOrder(OrderModel orderModel);

    }
}
