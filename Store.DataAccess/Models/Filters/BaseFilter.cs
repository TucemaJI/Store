namespace Store.DataAccess.Models.Filters
{
    public abstract class BaseFilter
    {
        public EntityParameters EntityParameters { get; set; }
        public bool IsDescending { get; set; }
        public string OrderByString { get; set; }
    }
}
