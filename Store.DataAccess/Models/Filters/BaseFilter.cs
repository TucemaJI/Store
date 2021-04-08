using Store.Shared.Options;

namespace Store.DataAccess.Models.Filters
{
    public abstract class BaseFilter
    {
        public PageOptions PageOptions { get; set; }
        public bool IsDescending { get; set; }
        public string OrderByField { get; set; }
    }
}
