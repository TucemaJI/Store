using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
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

        public async Task<List<Author>> GetAuthorListAsync(AuthorFilter filter)
        {
            var query = _dbSet.Include(item => item.AuthorInPrintingEditions).ThenInclude(i => i.PrintingEdition)
                .Where(a => EF.Functions.Like(a.Name, $"%{filter.Name}%"))
                .Where(a => filter.Id == 0 || a.Id.ToString().Contains(filter.Id.ToString()));
            var authors = await GetSortedListAsync(filter, query);
            filter.PageOptions.TotalItems = await query.CountAsync();
            return authors;
        }

        public Task<bool> ExistAsync(List<long> ids)
        {
            var result = _dbSet.AnyAsync(author => ids.Contains(author.Id));
            return result;
        }
    }
}
