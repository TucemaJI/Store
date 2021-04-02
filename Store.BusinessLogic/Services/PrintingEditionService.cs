using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly PrintingEditionMapper _printingEditionMapper;
        private readonly ConverterProvider _converterProvider;
        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository, PrintingEditionMapper printingEditionMapper,
            ConverterProvider converterProvider, IAuthorInPrintingEditionRepository authorInPrintingEditionRepository, IAuthorRepository authorRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _printingEditionMapper = printingEditionMapper;
            _converterProvider = converterProvider;
            _authorInPrintingEditionRepository = authorInPrintingEditionRepository;
            _authorRepository = authorRepository;
        }
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            var exist = await _authorRepository.ExistAsync(model.AuthorsIdList);

            if (exist is false)
            {
                model.Errors.Add(ExceptionConsts.AUTHOR_NOT_FOUND);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            var printingEdition = _printingEditionMapper.Map(model);

            await _printingEditionRepository.CreateAsync(printingEdition);

            var authorInPrintingEditionList = new List<AuthorInPrintingEdition>();

            model.AuthorsIdList.ForEach(authorId => authorInPrintingEditionList.Add(new AuthorInPrintingEdition { AuthorId = authorId, PrintingEditionId = printingEdition.Id }));

            await _authorInPrintingEditionRepository.CreateRangeAsync(authorInPrintingEditionList);
        }

        public async Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            var printingEdition = await GetPrintingEditionAsync(id);
            var result = _printingEditionMapper.Map(printingEdition);
            return result;
        }

        public async Task UpdatePrintingEdition(PrintingEditionModel printingEditionModel)
        {
            var printingEditionEntity = await GetPrintingEditionAsync(printingEditionModel.Id);
            var exist = await _authorRepository.ExistAsync(printingEditionModel.AuthorsIdList);

            if (exist is false)
            {
                printingEditionModel.Errors.Add(ExceptionConsts.AUTHOR_NOT_FOUND);
                throw new BusinessLogicException(printingEditionModel.Errors.ToList());
            }

            _printingEditionMapper.MapExist(printingEditionModel, printingEditionEntity);
            _printingEditionRepository.CleanRelations(printingEditionEntity);
            printingEditionModel.AuthorsIdList.ForEach(authorId => printingEditionEntity.AuthorsInPrintingEdition.Add(new AuthorInPrintingEdition { AuthorId = authorId, PrintingEditionId = printingEditionEntity.Id }));

            await _printingEditionRepository.UpdateAsync(printingEditionEntity);
        }

        public async Task DeletePrintingEditionAsync(long id)
        {
            var printingEditionEntity = await GetPrintingEditionAsync(id);
            await _printingEditionRepository.DeleteAsync(printingEditionEntity);
        }

        public async Task<PageModel<PrintingEditionModel>> GetPrintingEditionModelListAsync(PrintingEditionFilter filter)
        {
            var sortedPrintingEditions = await _printingEditionRepository.GetPrintingEditionListAsync(filter);
            var printingEditionModels = _printingEditionMapper.Map(sortedPrintingEditions);
            if (filter.Currency == CurrencyType.None)
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
            var pageModel = new PageModel<PrintingEditionModel>(printingEditionModels, filter.PageOptions);
            pageModel.MaxPrice = await _printingEditionRepository.GetMaxPriceAsync();
            pageModel.MinPrice = await _printingEditionRepository.GetMinPriceAsync();
            return pageModel;
        }

        private async Task<PrintingEdition> GetPrintingEditionAsync(long id)
        {
            var printingEdition = await _printingEditionRepository.GetItemAsync(id);
            if (printingEdition is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.PRINTING_EDITION_NOT_FOUND });
            }
            return printingEdition;
        }
    }
}
