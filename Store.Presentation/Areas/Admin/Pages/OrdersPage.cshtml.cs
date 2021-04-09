using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class OrdersPageModel : PageModel
    {
        private readonly IOrderService _orderService;
        public List<OrderModel> OrderList { get; set; }
        [BindProperty]
        public OrderFilter OrderFilter { get; set; }
        [BindProperty]
        public bool Paid { get; set; }
        [BindProperty]
        public bool Unpaid { get; set; }

        public OrdersPageModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task OnGetAsync()
        {
            OrderFilter = new OrderFilter();
            OrderFilter.PageOptions = new PageOptions();
            var orderList = await _orderService.GetOrderModelListAsync(OrderFilter);
            OrderList = orderList.Elements;
            OrderFilter.PageOptions = orderList.PageOptions;
        }
        public async Task<PageResult> OnPostAsync(string orderByString, int? pageIndex)
        {
            if (pageIndex is not null)
            {
                OrderFilter.PageOptions.CurrentPage = (int)pageIndex;
            }

            OrderFilter.Status = Paid ? StatusType.Paid : StatusType.None;
            OrderFilter.Status = Unpaid ? StatusType.Unpaid : OrderFilter.Status;
            OrderFilter.Status = Paid && Unpaid ? StatusType.None : OrderFilter.Status;

            if (!string.IsNullOrWhiteSpace(orderByString))
            {
                OrderFilter.OrderByField = orderByString;
            }
            var orderList = await _orderService.GetOrderModelListAsync(OrderFilter);
            OrderList = orderList.Elements;
            OrderFilter.PageOptions = orderList.PageOptions;
            return Page();

        }
    }
}
