namespace Store.DataAccess.Models.Filters
{
    public class AuthorFilter
    {
        public string Name { get; set; }
        public AuthorFilter()
        {
            Name = string.Empty;
        }
    }
}
