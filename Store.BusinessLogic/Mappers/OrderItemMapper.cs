using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class OrderItemMapper : BaseMapper<OrderItem, OrderItemModel>
    {
        public override OrderItem Map(OrderItemModel element)
        {
            return new OrderItem
            {
                Amount = element.Amount,
                Count = element.Count,
                Currency = element.Currency,
                IsRemoved = element.IsRemoved,
                OrderId = element.OrderId,
                PrintingEditionId = element.PrintingEditionId,
            };
        }

        public override OrderItemModel Map(OrderItem element)
        {
            return new OrderItemModel
            {
                Amount = element.Amount,
                Count = element.Count,
                Currency = element.Currency,
                IsRemoved = element.IsRemoved,
                OrderId = element.OrderId,
                PrintingEditionId = element.PrintingEditionId,
                CreationDate = element.CreationData,
                Type = element.PrintingEdition.Type,
                Title = element.PrintingEdition.Title,
            };
        }
    }
}
