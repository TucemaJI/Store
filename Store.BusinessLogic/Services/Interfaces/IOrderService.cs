using Store.BusinessLogic.Models.Orders;
using System;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService 
    {
        public OrderModel GetOrder(long id);
    }
}
