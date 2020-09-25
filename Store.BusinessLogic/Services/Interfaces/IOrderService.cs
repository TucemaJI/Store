using Store.BusinessLogic.Models.Orders;
using System;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public abstract Task<OrderModel> GetModelAsync(long id);
    }
}
