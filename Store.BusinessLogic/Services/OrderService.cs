using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPaymentRepository _paymentRepository;

        private readonly OrderMapper _orderMapper;
        private readonly OrderItemMapper _orderItemMapper;
        private readonly PaymentMapper _paymentMapper;

        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository,
            IOrderItemRepository orderItemRepository, OrderMapper orderMapper, PaymentMapper paymentMapper,
            OrderItemMapper orderItemMapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _paymentRepository = paymentRepository;

            _orderMapper = orderMapper;
            _orderItemMapper = orderItemMapper;
            _paymentMapper = paymentMapper;
        }
        public async Task CreateOrderAsync(OrderModel model)
        {
            var order = _orderMapper.Map(model);
            await _orderItemRepository.CreateOrderItemsAsync(order.OrderItems);
            await _orderRepository.CreateAsync(order);
        }

        public async Task<PageModel<OrderModel>> GetOrderModelsAsync(OrderFilter filter)
        {
            var orderList = await _orderRepository.GetFilterSortedListAsync(filter);
            var orderModelList = _orderMapper.Map(orderList);
            var pagedList = PagedList<OrderModel>.ToPagedList(orderModelList, orderList.Count, filter.EntityParameters.CurrentPage, filter.EntityParameters.ItemsPerPage);
            var pageModel = new PageModel<OrderModel>(pagedList);
            return pageModel;
        }

        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            var order = await _orderRepository.GetItemAsync(id);
            return _orderMapper.Map(order);
        }

        public Task DeleteOrderAsync(long id)
        {
            return _orderRepository.DeleteAsync(id);
        }

        public void UpdateOrder(OrderModel orderModel)
        {
            var order = _orderMapper.Map(orderModel);
            _orderRepository.UpdateAsync(order);
        }
    }
}
