using Store.Shared.Options;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models
{
    public class PageModel<T> where T : class
    {
        public List<T> Elements { get; set; }
        public PageOptions PageOptions { get; set; }
        public PageModel(List<T> pagedList, PageOptions parameters)
        {
            Elements = pagedList;

            PageOptions = parameters;
        }
    }
}
