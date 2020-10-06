using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<List<Author>> GetListAsync(EntityParameters entityParameters);
    }
}
