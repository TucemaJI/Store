using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public Task<long> CreateOrderAsync([FromBody] OrderModel model)
        {
            var result = _orderService.CreateOrderAsync(model);
            return result;
        }

        [Authorize]
        [HttpPost("GetOrders")]
        public Task<PageModel<OrderModel>> GetOrderModelsAsync([FromBody] OrderFilter filter)
        {
            var result = _orderService.GetOrderModelListAsync(filter);
            return result;
        }

        [Authorize]
        [HttpGet("GetOrder")]
        public Task<OrderModel> GetOrderModelAsync(long id)
        {
            var result = _orderService.GetOrderModelAsync(id);
            return result;
        }

        [Authorize]
        [HttpPost("DeleteOrder")]
        public async Task DeleteOrderAsync(long id)
        {
            await _orderService.DeleteOrderAsync(id);
        }

        [Authorize]
        [HttpPost("Pay")]
        public Task<bool> PayAsync([FromBody] OrderPayModel model)
        {
            var result = _orderService.PayAsync(model);
            return result;
        }
    }
}
