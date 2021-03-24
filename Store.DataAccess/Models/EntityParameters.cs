using Store.Shared.Constants;

namespace Store.DataAccess.Models
{
    public class EntityParameters //TO SHARED + RENAME
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }

        private int _itemsPerPage;
        public int ItemsPerPage
        {
            get => _itemsPerPage;

            set => _itemsPerPage = (value > EntityParametersOptions.MAX_PAGE_SIZE) ? EntityParametersOptions.MAX_PAGE_SIZE : value;

        }
        public EntityParameters()
        {
            CurrentPage = PagedListOptions.FIRST_PAGE;
            ItemsPerPage = PagedListOptions.DEFAULT_PAGE_SIZE;
        }
    }
}
