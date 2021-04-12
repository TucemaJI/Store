using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id);
        public Task CreatePrintingEditionAsync(PrintingEditionModel model);
        public Task<PageModel<PrintingEditionModel>> GetPrintingEditionModelListAsync(PrintingEditionFilter filter);
        public Task DeletePrintingEditionAsync(long id);
        public Task UpdatePrintingEditionAsync(PrintingEditionModel printingEditionModel);
    }
}
