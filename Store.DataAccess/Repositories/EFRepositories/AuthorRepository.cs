using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<List<Author>> GetListAsync(EntityParameters entityParameters)
        {
            var authors = await GetListAsync();
            return authors
                .OrderBy(on => on.Name)
                .Skip((entityParameters.PageNumber - 1) * entityParameters.PageSize)
                .Take(entityParameters.PageSize)
                .ToList();
        }
    }
}
