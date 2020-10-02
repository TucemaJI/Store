using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository<PrintingEdition> _printingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository<PrintingEdition> printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = new PrintingEditionMapper().Map(model);
            await _printingEditionRepository.CreateAsync(printingEdition);
            await _printingEditionRepository.SaveAsync();
        }

        public async Task<IEnumerable<PrintingEditionModel>> GetPrintingEditionModelsAsync()
        {
            var printingEditions = await _printingEditionRepository.GetListAsync();
            var printingEditionModels = new List<PrintingEditionModel>();
            foreach(var pe in printingEditions)
            {
                var printingEditionModel = new PrintingEditionMapper().Map(pe);
                printingEditionModels.Add(printingEditionModel);
            }
            return printingEditionModels;
        }

        public async Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            var printingEdition = await _printingEditionRepository.GetItemAsync(id);
            return new PrintingEditionMapper().Map(printingEdition);
        }
    }
}
