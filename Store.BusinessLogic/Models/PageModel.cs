using Store.DataAccess.Models;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models
{
    public class PageModel<T> where T : class
    {
        public List<T> Elements { get; set; }
        public EntityParameters PageParameters {get;set;}
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public PageModel(PagedList<T> pagedList)
        {
            Elements = new List<T>(pagedList);

            PageParameters = new EntityParameters
            {
                TotalItems = pagedList.TotalCount,
                CurrentPage = pagedList.CurrentPage,
                ItemsPerPage = pagedList.PageSize,

            };
        }
    }
}
