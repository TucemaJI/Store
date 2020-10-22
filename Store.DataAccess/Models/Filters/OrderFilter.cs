namespace Store.DataAccess.Models.Filters
{
    public class OrderFilter
    {
        public string Payment { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
    }
}
