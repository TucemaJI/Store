using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseRepository<PrintingEdition>
    {
        public IQueryable<PrintingEdition> GetFilteredList(PrintingEditionFilter filter);
        public Task<double> GetMaxPriceAsync();
        public Task<double> GetMinPriceAsync();
    }
}
