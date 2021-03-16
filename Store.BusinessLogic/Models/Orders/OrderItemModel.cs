using Store.BusinessLogic.Models.Base;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModel : BaseModel
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public long PrintingEditionId { get; set; }
        public long OrderId { get; set; }
        public long Count { get; set; }
        public PrintingEditionType Type { get; set; }
        public string Title { get; set; }
    }
}
