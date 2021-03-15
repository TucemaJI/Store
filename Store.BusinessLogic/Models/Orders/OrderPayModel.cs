using Store.BusinessLogic.Models.Orders;

namespace Store.BusinessLogic.Models
{
    public class OrderPayModel
    {
        public string Cardnumber { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public long Cvc { get; set; }
        public long Value { get; set; }
        public long OrderId { get; set; }
    }
}
