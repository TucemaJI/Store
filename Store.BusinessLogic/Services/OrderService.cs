using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Constants;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

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
        public async Task<long> CreateOrderAsync(OrderModel model)
        {
            var order = _orderMapper.Map(model);
            await _orderItemRepository.CreateOrderItemsAsync(order.OrderItems);
            var createdOrder = await _orderRepository.CreateAsync(order);
            return createdOrder.Id;
        }

        public async Task Pay(OrderPayModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Cardnumber) || string.IsNullOrWhiteSpace(model.Cardnumber) ||
                model.Month <= 0 || model.Month > 12 || model.OrderId <= 0 || model.Value <= 0 || model.Year <= 0)
            {
                throw new BusinessLogicException(ExceptionOptions.WRONG_PAYMENT);
            };

            StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";

            var optionsToken = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = model.Cardnumber,
                    ExpMonth = model.Month,
                    ExpYear = model.Year,
                    Cvc = model.Cvc,
                }
            };

            var serviceToken = await new TokenService().CreateAsync(optionsToken);

            var order = await _orderRepository.GetItemAsync(model.OrderId);

            var options = new ChargeCreateOptions
            {
                Amount = model.Value,
                Currency = CurrencyType.USD.ToString(),
                Description = order.Description,
                Source = serviceToken.Id,
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            if (charge.Paid)
            {
                var payment = new Payment() {
                    TransactionId = charge.Id,
                };
                payment = await _paymentRepository.CreateAsync(payment);
                order.Status = StatusType.Paid;
                order.PaymentId = payment.Id;
                _orderRepository.UpdateAsync(order);
            }
        }
        public async Task<PageModel<OrderModel>> GetOrderModelsAsync(OrderFilter filter)
        {
            var orderList = _orderRepository.GetFilteredList(filter);
            var sortedOrders = await _orderRepository.GetSortedListAsync(filter: filter, ts: orderList);
            var orderModelList = _orderMapper.Map(sortedOrders);
            var pagedList = PagedList<OrderModel>.ToPagedList(orderModelList, orderList.Count(), filter.EntityParameters.CurrentPage, filter.EntityParameters.ItemsPerPage);
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
