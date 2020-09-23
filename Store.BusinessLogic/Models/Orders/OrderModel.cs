using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public long PaymentId { get; set; }

    }
}
