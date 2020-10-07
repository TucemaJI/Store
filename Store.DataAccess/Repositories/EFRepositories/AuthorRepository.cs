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
        private readonly ApplicationContext _applicationContext;
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext) { _applicationContext = applicationContext; }

        public async Task<PagedList<Author>> GetListAsync(EntityParameters entityParameters)
        {
            var authors = _applicationContext.Authors.Where(x => EF.Functions.Like(x.GetType().GetProperty("Name").Name, "%Troelsen%")/*((string)x.GetType().GetProperty("Name").GetValue(x, null)).Contains("Troelsen")*/);
            var temp = authors.FirstOrDefault();
            return PagedList<Author>.ToPagedList(authors.OrderBy(on => on.Name).ToList()
                , entityParameters.PageNumber
                , entityParameters.PageSize);
        }
    }
}
