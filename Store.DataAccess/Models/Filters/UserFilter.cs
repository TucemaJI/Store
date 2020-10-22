namespace Store.DataAccess.Models.Filters
{
    public class UserFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserFilter()
        {
            Name = string.Empty;
            Email = string.Empty;
        }
    }
}
