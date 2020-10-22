using Store.Shared.Constants;

namespace Store.DataAccess.Models
{
    public class EntityParameters // todo magic numbers
    {
        public int PageNumber { get; set; }
        private int _pageSize = PagedListOptions.DefaultPageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > EntityParametersOptions.MaxPageSize) ? EntityParametersOptions.MaxPageSize : value;
            }
        }
        public EntityParameters()
        {
            PageNumber = PagedListOptions.FirstPage;
        }
    }
}
