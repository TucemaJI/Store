using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;

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
                CreationDate = element.CreationData,
                OrderItemModels = new OrderItemMapper().Map(element.OrderItems),
                Status = element.Status,
            };
        }
    }
}
