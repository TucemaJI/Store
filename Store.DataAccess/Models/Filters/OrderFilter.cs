namespace Store.DataAccess.Models.Filters
{
    public class OrderFilter
    {
        public string Payment { get; set; } 
        public string Status { get; set; } 
        public string Currency { get; set; }
        public OrderFilter()
        {
            Payment = string.Empty;
            Status = string.Empty;
            Currency = string.Empty;
        }
    }
}
