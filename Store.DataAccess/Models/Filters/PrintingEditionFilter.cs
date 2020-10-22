namespace Store.DataAccess.Models.Filters
{
    public class PrintingEditionFilter
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        
        public PrintingEditionFilter()
        {
            Name = string.Empty;
            Title = string.Empty;
            Currency = string.Empty;
            Type = string.Empty;
            MinPrice = string.Empty;
            MaxPrice = string.Empty;
        }
    }
}
