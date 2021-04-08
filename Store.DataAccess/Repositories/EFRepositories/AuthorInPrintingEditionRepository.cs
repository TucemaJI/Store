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
        public async Task CreateRangeAsync(List<long> authorIdList, long printingEditionId)
        {
            var authorsInPrintingEdition = new List<AuthorInPrintingEdition>();
            authorIdList.ForEach(authorId => authorsInPrintingEdition.Add(new AuthorInPrintingEdition { AuthorId = authorId, PrintingEditionId = printingEditionId }));

            await _dbSet.AddRangeAsync(authorsInPrintingEdition);
            await SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<AuthorInPrintingEdition> authorInPrintingEditionList)
        {
            _dbSet.RemoveRange(authorInPrintingEditionList);
            await SaveChangesAsync();
        }
    }
}
