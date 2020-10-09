using Store.Shared.Constants;

namespace Store.DataAccess.Models
{
    public class EntityParameters
    {
        public int PageNumber { get; set; }
        private int _pageSize = 10;
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
            PageNumber = 1;
        }
    }
}
