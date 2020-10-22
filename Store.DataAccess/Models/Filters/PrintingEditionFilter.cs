namespace Store.DataAccess.Models.Filters
{
    public class PrintingEditionFilter
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string MinPrice { get; set; } = string.Empty;
        public string MaxPrice { get; set; } = string.Empty;
    }
}
