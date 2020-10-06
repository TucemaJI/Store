using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService, ILogger<OrderController> logger) : base(logger)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task CreateOrderAsync(OrderModel model)
        {
            await _orderService.CreateOrderAsync(model);
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<List<OrderModel>> GetOrderModelsAsync()
        {
            return await _orderService.GetOrderModelsAsync();
        }

        [Authorize]
        [HttpGet("GetOrder")]
        public async Task<OrderModel> GetOrderModelAsync(long id)
        {
            return await _orderService.GetOrderModelAsync(id);
        }

        [Authorize]
        [HttpDelete("DeleteOrder")]
        public async Task DeleteOrderAsync(long id)
        {
            await _orderService.DeleteOrderAsync(id);
        }

        [Authorize]
        [HttpPost("UpdateOrder")]
        public void UpdateOrder(OrderModel orderModel)
        {
            _orderService.UpdateOrder(orderModel);
        }
    }
}
