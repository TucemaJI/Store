using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository<PrintingEdition> _printingEditionRepository;
        private readonly PrintingEditionMapper _printingEditionMapper;
        public PrintingEditionService(IPrintingEditionRepository<PrintingEdition> printingEditionRepository, PrintingEditionMapper printingEditionMapper)
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
            var printingEditionModels = new List<PrintingEditionModel>();
            foreach(var pe in printingEditions)
            {
                var printingEditionModel = _printingEditionMapper.Map(pe);
                printingEditionModels.Add(printingEditionModel);
            }
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
        }

        public async Task DeletePrintingEditionAsync(long id)
        {
            await _printingEditionRepository.DeleteAsync(id);
            await _printingEditionRepository.SaveAsync();
        }

        public async Task<List<PrintingEditionModel>> FilterPrintingEditionsAsync(string filter, string filterBy) // throw it to repository(don't know how)
        {
            var printingEditionModels = await GetPrintingEditionModelsAsync();
            switch (filterBy)
            {
                case "Title":
                    return printingEditionModels
                        .Where(x => x.Title.Contains(filter))
                        .ToList();
                case "Currency":
                    return printingEditionModels
                        .Where(x => x.Currency == Enum.Parse<Currency>(filter))
                        .ToList();
                case "Status":
                    return printingEditionModels
                        .Where(x => x.Status == Enum.Parse<Status>(filter))
                        .ToList();
                case "Type":
                    return printingEditionModels
                        .Where(x => x.Type == Enum.Parse<PrintingEditionType>(filter))
                        .ToList();
                default:
                    break;
            }
            throw new BusinessLogicException("Problem with user filtration");
        }
    }
}
