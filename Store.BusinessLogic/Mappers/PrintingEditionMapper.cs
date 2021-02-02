using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class PrintingEditionMapper : BaseMapper<PrintingEdition, PrintingEditionModel>
    {
        public override PrintingEdition Map(PrintingEditionModel element)
        {
            return new PrintingEdition
            {
                ReturnedCurrency = element.Currency,
                Description = element.Description,
                IsRemoved = element.IsRemoved,
                Price = element.Price,
                Title = element.Title,
                Type = element.Type,
            };
        }

        public override PrintingEditionModel Map(PrintingEdition element)
        {
            return new PrintingEditionModel
            {
                Currency = element.ReturnedCurrency,
                Description = element.Description,
                IsRemoved = element.IsRemoved,
                Price = element.Price,
                Title = element.Title,
                Type = element.Type,
                CreationDate = element.CreationData,
            };
        }
    }
}
