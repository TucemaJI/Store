using Store.DataAccess.Models;
using Store.Shared.Options;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models
{
    public class PageModel<T> where T : class
    {
        public List<T> Elements { get; set; }
        public PageOptions PageParameters { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public PageModel(List<T> pagedList, PageOptions parameters)
        {
            Elements = pagedList;

            PageParameters = parameters;
        }
    }
}
