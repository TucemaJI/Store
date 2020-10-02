using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class OrderService :  IOrderService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        public OrderService(IOrderRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task CreateOrderAsync(OrderModel model)
        {
            var order = new OrderMapper().Map(model);
            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();
        }

        public  async Task<IEnumerable<OrderModel>> GetOrderModelsAsync()
        {
            var orderList = await _orderRepository.GetListAsync();
            var orderModelList = new List<OrderModel>();
            foreach (var order in orderList)
            {
                var orderModel = new OrderMapper().Map(order);
                orderModelList.Add(orderModel);
            }
            return orderModelList;
        }

        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            var order = await _orderRepository.GetItemAsync(id);
            return new OrderMapper().Map(order);
        }
    }
}
