using Store.Shared.Options;

namespace Store.DataAccess.Models.Filters
{
    public abstract class BaseFilter
    {
        public PageOptions EntityParameters { get; set; }
        public bool IsDescending { get; set; }
        public string OrderByString { get; set; }
    }
}
