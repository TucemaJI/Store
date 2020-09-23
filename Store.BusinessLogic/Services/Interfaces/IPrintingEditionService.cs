using Store.BusinessLogic.Models.PrintingEditions;
using System;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService 
    {
        public PrintingEditionModel GetOrder(long id);
    }
}
