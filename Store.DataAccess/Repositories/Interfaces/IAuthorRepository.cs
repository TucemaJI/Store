using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public IQueryable<Author> GetFilteredQuery(AuthorFilter filter);
        public Task<bool> ExistAsync(List<long> ids);
    }
}
