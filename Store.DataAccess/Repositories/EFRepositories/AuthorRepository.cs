using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<(List<Author> authorList, long count)> GetAuthorListAsync(AuthorFilter filter)
        {
            var query = _dbSet.Include(item => item.AuthorInPrintingEditions).ThenInclude(i => i.PrintingEdition)
                .Where(a => EF.Functions.Like(a.Name, $"%{filter.Name}%"))
                .Where(a => filter.Id == RepositoryConsts.NULL_ID || a.Id.ToString().Contains(filter.Id.ToString()))
                .AsNoTracking();
            var authors = await GetSortedListAsync(filter, query);
            var result = (authorList: authors, count: await query.CountAsync());
            return result;
        }
        public async Task<List<Author>> GetAuthorListAsync()
        {
            var result = await _dbSet.Select(item => item).AsNoTracking().ToListAsync();
            return result;
        }

        public Task<bool> ExistAsync(List<long> ids)
        {
            var result = _dbSet.AnyAsync(author => ids.Contains(author.Id));
            return result;
        }

        public override Task<Author> GetItemAsync(long id)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
