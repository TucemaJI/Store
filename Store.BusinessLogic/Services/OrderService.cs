using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Stripe;
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

        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository,
            IOrderItemRepository orderItemRepository, OrderMapper orderMapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _paymentRepository = paymentRepository;

            _orderMapper = orderMapper;
        }
        public async Task<long> CreateOrderAsync(OrderModel model)
        {
            var orderEntity = _orderMapper.Map(model);
            orderEntity.Payment = new Payment();
            await _paymentRepository.CreateAsync(orderEntity.Payment);
            orderEntity.Status = StatusType.Unpaid;
            await _orderRepository.CreateAsync(orderEntity);
            await _orderItemRepository.CreateOrderItemsAsync(orderEntity.OrderItems);
            return orderEntity.Id;// Check if id exist in debugger
        }

        public async Task<bool> Pay(OrderPayModel model)
        {
            //StripeConfiguration.ApiKey = OrderServiceOptions.API_KEY;// TO STARTUP

            var cvc = model.Cvc.ToString();
            var optionsToken = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = model.Cardnumber,
                    ExpMonth = model.Month,
                    ExpYear = model.Year,
                    Cvc = cvc,
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
                Source = stripeToken.Id,// try not use source (token from angular)
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
        public async Task<PageModel<OrderModel>> GetOrderModelsAsync(OrderFilter filter)
        {
            var orderQuery = _orderRepository.GetFilteredQuery(filter);
            var sortedOrders = await _orderRepository.GetSortedListAsync(filter: filter, query: orderQuery);
            var orderModelList = _orderMapper.Map(sortedOrders);
            var pagedList = PagedList<OrderModel>.ToPagedList(orderModelList, orderQuery.Count(), filter.EntityParameters.CurrentPage, filter.EntityParameters.ItemsPerPage);
            var pageModel = new PageModel<OrderModel>(pagedList);
            return pageModel;
        }

        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            var order = _orderRepository.GetItemAsync(id);
            return _orderMapper.Map(await order);
        }

        public Task DeleteOrderAsync(long id)
        {
            return _orderRepository.DeleteAsync(id);
        }

    }
}
