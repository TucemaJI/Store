using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public IQueryable<PrintingEdition> GetFilteredList(PrintingEditionFilter filter)
        {
            var printingEditions = _dbSet.Include(item => item.AuthorsInPrintingEdition)
                .ThenInclude(item => item.Author)

                //.Where(pE => pE.Type == (filter.PEType == 0 ?  pE.Type : filter.PEType))
                .Where(pE => filter.PEType.Contains(PrintingEditionType.None) || filter.PEType.Contains(pE.Type))
                .Where(pE => filter.MaxPrice >= pE.Price && pE.Price >= filter.MinPrice)
                .Where(pE => EF.Functions.Like(pE.Title, $"%{filter.Title}%") || pE.AuthorsInPrintingEdition.Any(aipe => EF.Functions.Like(aipe.Author.Name, $"%{filter.Name}%")));
                //.Where(pE => pE.AuthorsInPrintingEdition.Any(aipe => EF.Functions.Like(aipe.Author.Name, $"%{filter.Name}%")));
            return printingEditions;
        }
        public Task<double> GetMaxPriceAsync() {
            var maxPrice = _dbSet.MaxAsync(pE => pE.Price);
            return maxPrice;
        }
        public Task<double> GetMinPriceAsync()
        {
            var minPrice = _dbSet.MinAsync(pE => pE.Price);
            return minPrice;
        }
    }
}
