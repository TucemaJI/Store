using Store.DataAccess.Models.Filters;
using Store.Shared.Constants;

namespace Store.DataAccess.Models
{
    public class EntityParameters
    {
        public int PageNumber { get; set; }

        private int _pageSize = PagedListOptions.DEFAULT_PAGE_SIZE;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > EntityParametersOptions.MAX_PAGE_SIZE) ? EntityParametersOptions.MAX_PAGE_SIZE : value;
            }
        }
        public EntityParameters()
        {
            PageNumber = PagedListOptions.FIRST_PAGE;
        }
    }
}
