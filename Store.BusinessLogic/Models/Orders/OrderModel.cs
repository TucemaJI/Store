using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public double TotalAmount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long PaymentId { get; set; }
        public StatusType Status { get; set; }
        [Required]
        public List<OrderItemModel> OrderItemModels { get; set; }
    }
}
