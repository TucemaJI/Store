using Store.DataAccess.Entities.Base;
using System.Collections.Generic;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long PaymentId { get; set; }
        public Payment Payment { get; set; }
        public StatusType Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
