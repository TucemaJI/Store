using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<PagedList<Author>> GetFilterSortedPagedListAsync(AuthorFilter filter);
    }
}
