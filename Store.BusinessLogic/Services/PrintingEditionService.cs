using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly PrintingEditionMapper _printingEditionMapper;
        private readonly ConverterProvider _converterProvider;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository, PrintingEditionMapper printingEditionMapper, ConverterProvider converterProvider)
        {
            _printingEditionRepository = printingEditionRepository;
            _printingEditionMapper = printingEditionMapper;
            _converterProvider = converterProvider;
        }
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            var printingEdition = _printingEditionMapper.Map(model);
            await _printingEditionRepository.CreateAsync(printingEdition);
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
            _printingEditionRepository.UpdateAsync(printingEdition);
        }

        public async Task DeletePrintingEditionAsync(long id)
        {
            await _printingEditionRepository.DeleteAsync(id);
        }

        public async Task<PageModel<PrintingEditionModel>> GetPrintingEditionModelsAsync(PrintingEditionFilter filter)
        {
            var printingEditions =  _printingEditionRepository.GetFilteredList(filter);
            var sortedPrintingEditions = await _printingEditionRepository.GetSortedListAsync(filter: filter, ts: printingEditions);
            var printingEditionModels = _printingEditionMapper.Map(sortedPrintingEditions);
            if(filter.Currency == CurrencyType.None)
            {
                filter.Currency = CurrencyType.USD;
            }
            if (filter.Currency != CurrencyType.USD)
            {
                foreach (var element in printingEditionModels)
                {
                    element.Price = await _converterProvider.ConvertAsync(filter.Currency, element.Price);
                    element.Currency = filter.Currency;
                }
            }
            var pagedList = PagedList<PrintingEditionModel>.ToPagedList(printingEditionModels, printingEditions.Count(), filter.EntityParameters.CurrentPage, filter.EntityParameters.ItemsPerPage);
            var pageModel = new PageModel<PrintingEditionModel>(pagedList);
            pageModel.MaxPrice = await _printingEditionRepository.GetMaxPriceAsync();
            pageModel.MinPrice = await _printingEditionRepository.GetMinPriceAsync();
            return pageModel;
            throw new BusinessLogicException(ExceptionOptions.FILTRATION_PROBLEM);
        }
    }
}
