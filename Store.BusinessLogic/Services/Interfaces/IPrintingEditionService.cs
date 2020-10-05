using Store.BusinessLogic.Models.PrintingEditions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id);
        public Task CreatePrintingEditionAsync(PrintingEditionModel model);

        public Task<List<PrintingEditionModel>> GetPrintingEditionModelsAsync();
        public Task DeletePrintingEditionAsync(long id);
        public void UpdatePrintingEdition(PrintingEditionModel printingEditionModel);
    }
}
