namespace Store.DataAccess.Models.Filters
{
    public class UserFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
    }
}
