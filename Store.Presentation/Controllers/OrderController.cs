using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;

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
        
    }
}
