using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections;

namespace Store.BusinessLogic.Models.Orders
{
     public class OrderItemModel : BaseModel
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public long PrintingEditionId { get; set; }
        public PrintingEditionModel PrintingEditionModel { get; set; }
        public long OrderId { get; set; }
        public OrderModel OrderModel { get; set; }
        public long Count { get; set; }
    }
}
