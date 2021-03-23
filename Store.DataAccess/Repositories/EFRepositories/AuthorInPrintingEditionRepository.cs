using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorInPrintingEditionRepository : BaseEFRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository
    {
        public AuthorInPrintingEditionRepository(ApplicationContext appicationContext) : base(appicationContext) { }
        public Task CreateRangeAsync(List<AuthorInPrintingEdition> authorInPrintingEditionList)
        {
            var result = _dbSet.AddRangeAsync(authorInPrintingEditionList);
            return result;
        }
    }
}
