﻿using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;
using System.Linq;

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
                Id = element.Id,
                Currency = element.ReturnedCurrency,
                Description = element.Description,
                IsRemoved = element.IsRemoved,
                Price = element.Price,
                Title = element.Title,
                Type = element.Type,
                CreationDate = element.CreationData,
                Authors = element.AuthorsInPrintingEdition.Select(x => x.Author.Name).ToList(),
                AuthorsIdList = element.AuthorsInPrintingEdition.Select(x => x.Author.Id).ToList(),
            };
        }

        public void MapExist(PrintingEditionModel element, PrintingEdition printingEdition)
        {
            printingEdition.ReturnedCurrency = element.Currency;
            printingEdition.Description = element.Description;
            printingEdition.IsRemoved = element.IsRemoved;
            printingEdition.Price = element.Price;
            printingEdition.Title = element.Title;
            printingEdition.Type = element.Type;
        }
    }
}
