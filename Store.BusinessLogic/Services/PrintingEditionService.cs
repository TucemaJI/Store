using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : BaseService<PrintingEditionModel>, IPrintingEditionService
    {
        private readonly IPrintingEditionRepository<PrintingEdition> _printingEditionRepository;
        public PrintingEditionService(IPrintingEditionRepository<PrintingEdition> printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }
        public override void CreateEntity(PrintingEditionModel model)
        {

            _printingEditionRepository.Create(new PrintingEditionMapper().Map(model));
            _printingEditionRepository.Save();
        }

        public override IEnumerable<PrintingEditionModel> GetModels()
        {
            var printingEditions = _printingEditionRepository.GetList();
            var printingEditionModels = new List<PrintingEditionModel>();
            foreach(var pe in printingEditions)
            {
                printingEditionModels.Add(new PrintingEditionMapper().Map(pe));
            }
            return printingEditionModels;
        }

        public PrintingEditionModel GetOrder(long id)
        {
            return new PrintingEditionMapper().Map(_printingEditionRepository.GetItem(id));
        }
    }
}
