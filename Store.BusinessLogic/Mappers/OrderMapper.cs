using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;
using System.Linq;

namespace Store.BusinessLogic.Mappers
{
    public class OrderMapper : BaseMapper<Order, OrderModel>
    {
        public override Order Map(OrderModel element)
        {
            return new Order
            {
                Description = element.Description,
                IsRemoved = element.IsRemoved,
                PaymentId = element.PaymentId,
                UserId = element.UserId,
                OrderItems = new OrderItemMapper().Map(element.OrderItemModels),
                Status = element.Status,
            };
        }

        public override OrderModel Map(Order element)
        {
            return new OrderModel
            {
                Description = element.Description,
                IsRemoved = element.IsRemoved,
                PaymentId = element.PaymentId,
                UserId = element.UserId,
                UserName = $"{element.User.FirstName} {element.User.LastName}",
                UserEmail = element.User.Email,
                CreationDate = element.CreationData,
                OrderItemModels = new OrderItemMapper().Map(element.OrderItems),
                Status = element.Status,
                TotalAmount = element.OrderItems.Sum(x => x.Amount),
                Id = element.Id,
            };
        }
    }
}
