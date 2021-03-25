using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<PrintingEdition>
    {
        public (Task<List<PrintingEdition>>, Task<int>) GetPrintingEditionListAsync(PrintingEditionFilter filter);
        public Task<double> GetMaxPriceAsync();
        public Task<double> GetMinPriceAsync();
    }
}
