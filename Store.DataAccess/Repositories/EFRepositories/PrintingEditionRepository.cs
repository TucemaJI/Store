using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public Task<PagedList<PrintingEdition>> GetFilterSortedPagedListAsync(PrintingEditionFilter filter)
        {
            var printingEditions = _dbSet.Include(item => item.AuthorsInPrintingEdition)
                .ThenInclude(item => item.Author)
                .Where(pE => pE.Currency == filter.Currency)
                .Where(pE => EF.Functions.Like(pE.Title, $"%{filter.Title}%"))
                .Where(pE => pE.Type == filter.Type)
                .Where(pE => filter.MaxPrice >= pE.Price && pE.Price >= filter.MinPrice)
                .Where(pE => pE.AuthorsInPrintingEdition.Any(aipe => EF.Functions.Like(aipe.Author.Name, $"%{filter.Name}%")));
            var sortedPrintingEditions = GetSortedPagedList(filter: filter, ts: printingEditions);
            return sortedPrintingEditions;
        }
    }
}
