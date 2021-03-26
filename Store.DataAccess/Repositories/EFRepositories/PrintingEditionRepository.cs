using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
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

        public async Task<List<PrintingEdition>> GetPrintingEditionListAsync(PrintingEditionFilter filter)
        {
            var query = _dbSet.Include(item => item.AuthorsInPrintingEdition).ThenInclude(item => item.Author)
                .Where(printingEdition => filter.PrintingEditionTypeList.Contains(PrintingEditionType.None) || filter.PrintingEditionTypeList.Contains(printingEdition.Type))
                .Where(printingEdition => filter.MaxPrice >= printingEdition.Price && printingEdition.Price >= filter.MinPrice)
                .Where(printingEdition => EF.Functions.Like(printingEdition.Title, $"%{filter.Title}%") || printingEdition.AuthorsInPrintingEdition.Any(item => EF.Functions.Like(item.Author.Name, $"%{filter.Name}%")));
            var printingEditions = await GetSortedListAsync(filter, query);
            filter.EntityParameters.TotalItems = await query.CountAsync();
            return printingEditions;
        }
        public Task<double> GetMaxPriceAsync()
        {
            var maxPrice = _dbSet.MaxAsync(printingEdition => printingEdition.Price);
            return maxPrice;
        }
        public Task<double> GetMinPriceAsync()
        {
            var minPrice = _dbSet.MinAsync(printingEdition => printingEdition.Price);
            return minPrice;
        }
        public void CleanRelations(PrintingEdition printingEdition)
        {
            printingEdition.AuthorsInPrintingEdition.Clear();

        }
    }
}
