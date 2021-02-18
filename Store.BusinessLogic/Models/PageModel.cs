using Store.DataAccess.Models;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models
{
    public class PageModel<T> where T : class
    {
        public List<T> Elements { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public PageModel(PagedList<T> pagedList)
        {
            Elements = new List<T>(pagedList);

            CurrentPage = pagedList.CurrentPage;
            HasNext = pagedList.HasNext;
            HasPrevious = pagedList.HasPrevious;
            PageSize = pagedList.PageSize;
            TotalCount = pagedList.TotalCount;
            TotalPages = pagedList.TotalPages;
        }
    }
}
