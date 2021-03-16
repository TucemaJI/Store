using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Models.Filters
{
    public class OrderFilter : BaseFilter
    {
        public string UserId { get; set; }
        public StatusType Status { get; set; }
    }
}
