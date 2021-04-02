using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;

        private readonly OrderMapper _orderMapper;

        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, OrderMapper orderMapper)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;

            _orderMapper = orderMapper;
        }
        public async Task<long> CreateOrderAsync(OrderModel model)
        {
            var orderEntity = _orderMapper.Map(model);
            orderEntity.Payment = new Payment();
            orderEntity.Status = StatusType.Unpaid;
            await _orderRepository.CreateAsync(orderEntity);
            return orderEntity.Id;
        }

        public async Task<bool> Pay(OrderPayModel model)
        {
            var optionsToken = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = model.Cardnumber,
                    ExpMonth = model.Month,
                    ExpYear = model.Year,
                    Cvc = model.Cvc.ToString(),
                }
            };

            var serviceToken = new TokenService();
            var stripeToken = serviceToken.Create(optionsToken);

            var order = await _orderRepository.GetItemAsync(model.OrderId);

            var options = new ChargeCreateOptions
            {
                Amount = model.Value,
                Currency = CurrencyType.USD.ToString(),
                Description = order.Description,
                Source = stripeToken.Id,
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            if (charge.Paid)
            {
                var payment = await _paymentRepository.GetItemAsync(order.PaymentId);
                payment.TransactionId = charge.Id;
                await _paymentRepository.UpdateAsync(payment);
                order.Status = StatusType.Paid;
                order.PaymentId = payment.Id;
                await _orderRepository.UpdateAsync(order);
            }
            return charge.Paid;

        }
        public async Task<PageModel<OrderModel>> GetOrderModelListAsync(OrderFilter filter)
        {
            var sortedOrders = await _orderRepository.GetOrderListAsync(filter);
            var orderModelList = _orderMapper.Map(sortedOrders);
            var pageModel = new PageModel<OrderModel>(orderModelList, filter.PageOptions);
            return pageModel;
        }

        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            var order = _orderRepository.GetItemAsync(id);
            if (order is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.ORDER_NOT_FOUND });
            }
            var result = _orderMapper.Map(await order);
            return result;
        }

        public async Task DeleteOrderAsync(long id)
        {
            var order = await _orderRepository.GetItemAsync(id);

            if (order is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.ORDER_NOT_FOUND });
            }
            await _orderRepository.DeleteAsync(order);
        }

    }
}
