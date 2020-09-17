using Store.DataAccess.Entities.Base;

namespace Store.DataAccess.Entities
{
    public class OrderItem : BaseEntity
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public long PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long Count { get; set; }
    }
}
