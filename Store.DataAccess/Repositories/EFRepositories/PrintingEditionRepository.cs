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
                .Where(printingEdition => EF.Functions.Like(printingEdition.Title, $"%{filter.Title}%") || printingEdition.AuthorsInPrintingEdition.Any(item => EF.Functions.Like(item.Author.Name, $"%{filter.Name}%")))
                .AsNoTracking();
            var printingEditions = await GetSortedListAsync(filter, query);
            filter.PageOptions.TotalItems = await query.CountAsync();
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
        public async Task UpdateAsync(PrintingEdition item, List<long> authorIds)
        {

            //item.AuthorsInPrintingEdition.Clear();
            //authorIds.ForEach(authorId => item.AuthorsInPrintingEdition.Add(new AuthorInPrintingEdition { AuthorId = authorId, PrintingEditionId = item.Id }));
            _dbSet.Update(item);
            //var test =await _dbSet.Include(a => a.AuthorsInPrintingEdition).ThenInclude(a => a.Author).AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
            await SaveChangesAsync();
        }
        public override async Task<PrintingEdition> GetItemAsync(long id)
        {
            return await _dbSet.Include(a => a.AuthorsInPrintingEdition).ThenInclude(a => a.Author).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
