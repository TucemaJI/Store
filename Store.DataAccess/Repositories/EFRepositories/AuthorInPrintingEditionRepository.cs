using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorInPrintingEditionRepository : BaseEFRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository
    {
        public AuthorInPrintingEditionRepository(ApplicationContext appicationContext) : base(appicationContext) { }

        public async Task<List<AuthorInPrintingEdition>> GetListAsync(EntityParameters entityParameters)
        {
            var authorInPrintingEditions = await GetListAsync();
            return authorInPrintingEditions
                .OrderBy(on => on.PrintingEdition)
                .Skip((entityParameters.PageNumber - PagedListOptions.CorrectPageNumber) * entityParameters.PageSize)
                .Take(entityParameters.PageSize)
                .ToList();
        }
    }
}
