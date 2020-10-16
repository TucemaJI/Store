using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly PrintingEditionMapper _printingEditionMapper;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository, PrintingEditionMapper printingEditionMapper)
        {
            _printingEditionRepository = printingEditionRepository;
            _printingEditionMapper = printingEditionMapper;
        }
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = _printingEditionMapper.Map(model);
            await _printingEditionRepository.CreateAsync(printingEdition);
            await _printingEditionRepository.SaveAsync();
        }

        public async Task<List<PrintingEditionModel>> GetPrintingEditionModelsAsync()
        {
            var printingEditions = await _printingEditionRepository.GetListAsync();
            var printingEditionModels = _printingEditionMapper.Map(printingEditions);
            return printingEditionModels;
        }

        public async Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            var printingEdition = await _printingEditionRepository.GetItemAsync(id);
            return _printingEditionMapper.Map(printingEdition);
        }

        public void UpdatePrintingEdition(PrintingEditionModel printingEditionModel)
        {
            var printingEdition = _printingEditionMapper.Map(printingEditionModel);
            _printingEditionRepository.Update(printingEdition);
            _printingEditionRepository.SaveAsync();
        }

        public async Task DeletePrintingEditionAsync(long id)
        {
            await _printingEditionRepository.DeleteAsync(id);
            await _printingEditionRepository.SaveAsync();
        }

        public async Task<List<PrintingEditionModel>> FilterPrintingEditionsAsync(string filter, string filterBy) 
        {
            var printingEditionModels = await GetPrintingEditionModelsAsync();

            throw new BusinessLogicException(ExceptionOptions.ProblemWithUserFiltration);
        }
    }
}
