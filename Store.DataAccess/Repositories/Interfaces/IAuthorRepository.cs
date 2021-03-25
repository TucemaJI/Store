using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public (Task<List<Author>>, Task<int>) GetAuthorListAsync(AuthorFilter filter);
        public Task<bool> ExistAsync(List<long> ids);
    }
}
