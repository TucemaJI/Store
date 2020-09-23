using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class OrderService : BaseService<OrderModel>, IOrderService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        public OrderService(IOrderRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public override void CreateEntity(OrderModel model)
        {
            _orderRepository.Create(new OrderMapper().Map(model));
            _orderRepository.Save();
        }

        public override IEnumerable<OrderModel> GetModels()
        {
            var orderList = _orderRepository.GetList();
            var orderModelList = new List<OrderModel>();
            foreach (var order in orderList)
            {
                orderModelList.Add(new OrderMapper().Map(order));
            }
            return orderModelList;
        }

        public OrderModel GetOrder(long id)
        {
            return new OrderMapper().Map(_orderRepository.GetItem(id));
        }
    }
}
