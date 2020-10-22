using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<PagedList<Author>> GetListAsync(EntityParameters entityParameters)
        {
            
            var temp = _dbSet.FirstOrDefault();
            return PagedList<Author>.ToPagedList(_dbSet.OrderBy(on => on.Name).ToList()
                , entityParameters.PageNumber
                , entityParameters.PageSize);
        }
    }
}
