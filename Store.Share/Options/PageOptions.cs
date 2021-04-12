using static Store.Shared.Constants.Constants;

namespace Store.Shared.Options
{
    public class PageOptions
    {
        public int CurrentPage { get; set; }
        public long TotalItems { get; set; }

        private int _itemsPerPage;
        public int ItemsPerPage
        {
            get => _itemsPerPage;

            set => _itemsPerPage = (value > EntityParametersConsts.MAX_PAGE_SIZE) ? EntityParametersConsts.MAX_PAGE_SIZE : value;

        }

        public bool HasPrevious => CurrentPage > PagedListConsts.FIRST_PAGE;
        public bool HasNext => CurrentPage < TotalItems / _itemsPerPage;
        public PageOptions()
        {
            CurrentPage = PagedListConsts.FIRST_PAGE;
            ItemsPerPage = PagedListConsts.DEFAULT_PAGE_SIZE;
        }

    }
}
