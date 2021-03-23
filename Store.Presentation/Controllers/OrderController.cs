using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger) : base(logger)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public Task<long> CreateOrderAsync([FromBody] OrderModel model)
        {
            return _orderService.CreateOrderAsync(model);
        }

        [Authorize]
        [HttpPost("GetOrders")]
        public Task<PageModel<OrderModel>> GetOrderModelsAsync([FromBody] OrderFilter filter)
        {
            return _orderService.GetOrderModelsAsync(filter);
        }

        [Authorize]
        [HttpGet("GetOrder")]
        public Task<OrderModel> GetOrderModelAsync(long id)
        {
            return _orderService.GetOrderModelAsync(id);
        }

        [Authorize]
        [HttpPost("DeleteOrder")]
        public Task DeleteOrderAsync(long id)
        {
            return _orderService.DeleteOrderAsync(id);
        }

        [Authorize]
        [HttpPost("Pay")]
        public Task<bool> Pay([FromBody] OrderPayModel model)
        {
            return _orderService.Pay(model);
        }
    }
}
