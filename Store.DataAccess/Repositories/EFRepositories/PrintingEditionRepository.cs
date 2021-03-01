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

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public IQueryable<PrintingEdition> GetFilteredList(PrintingEditionFilter filter)
        {
            var printingEditions = _dbSet.Include(item => item.AuthorsInPrintingEdition)
                .ThenInclude(item => item.Author)
                //.Where(pE => pE.ReturnedCurrency == filter.Currency)
                .Where(pE => EF.Functions.Like(pE.Title, $"%{filter.Title}%"))
                .Where(pE => pE.Type == (filter.PEType == 0 ?  pE.Type : filter.PEType))
                //.Where(pE => filter.PEType == 0 || filter.PEType == pE.Type)
                .Where(pE => filter.MaxPrice >= pE.Price && pE.Price >= filter.MinPrice)
                .Where(pE => pE.AuthorsInPrintingEdition.Any(aipe => EF.Functions.Like(aipe.Author.Name, $"%{filter.Name}%")));
            return printingEditions;
        }
    }
}
