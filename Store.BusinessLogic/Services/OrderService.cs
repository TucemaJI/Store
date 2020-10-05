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
        private readonly OrderMapper _orderMapper;
        public OrderService(IOrderRepository<Order> orderRepository, OrderMapper orderMapper)
        {
            _orderRepository = orderRepository;
            _orderMapper = orderMapper;
        }
        public async Task CreateOrderAsync(OrderModel model)
        {
            var order = _orderMapper.Map(model);
            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();
        }

        public  async Task<IEnumerable<OrderModel>> GetOrderModelsAsync()
        {
            var orderList = await _orderRepository.GetListAsync();
            var orderModelList = new List<OrderModel>();
            foreach (var order in orderList)
            {
                var orderModel = _orderMapper.Map(order);
                orderModelList.Add(orderModel);
            }
            return orderModelList;
        }

        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            var order = await _orderRepository.GetItemAsync(id);
            return _orderMapper.Map(order);
        }

        public async Task DeleteOrderAsync(long id)
        {
            await _orderRepository.DeleteAsync(id);
            await _orderRepository.SaveAsync();
        }

        public void UpdateOrder(OrderModel orderModel)
        {
            var order = _orderMapper.Map(orderModel);
            _orderRepository.Update(order);
        }
    }
}
