using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public long PaymentId { get; set; }
        public Status Status { get; set; }
        public List<OrderItemModel> OrderItemModels { get; set; }
    }
}
