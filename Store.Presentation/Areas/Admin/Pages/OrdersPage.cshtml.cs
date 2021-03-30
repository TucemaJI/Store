using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
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
            if (Paid)
            {
                OrderFilter.Status = StatusType.Paid;
            }
            if (Unpaid)
            {
                OrderFilter.Status = StatusType.Unpaid;
            }
            if (Paid && Unpaid)
            {
                OrderFilter.Status = StatusType.None;
            }
            if (!string.IsNullOrWhiteSpace(orderByString))
            {
                OrderFilter.OrderByString = orderByString;
            }
            var orderList = await _orderService.GetOrderModelListAsync(OrderFilter);
            OrderList = orderList.Elements;
            OrderFilter.PageOptions = orderList.PageOptions;
            return Page();

        }
    }
}
