using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Models.Users;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public UserModel UserModel { get; set; }
        public long PaymentId { get; set; }
        public PaymentModel Payment { get; set; }
        public ICollection<OrderItemModel> OrderItemModels { get; set; }
        public OrderModel()
        {
            OrderItemModels = new List<OrderItemModel>();
        }
    }
}
