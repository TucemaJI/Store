using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<PagedList<Author>> GetListAsync(EntityParameters entityParameters);
    }
}
