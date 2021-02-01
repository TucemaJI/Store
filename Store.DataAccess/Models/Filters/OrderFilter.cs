using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Models.Filters
{
    public class OrderFilter : BaseFilter
    {
        public long Payment { get; set; } 
        public StatusType Status { get; set; } 
        public string Currency { get; set; }
    }
}
