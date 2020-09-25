using Store.BusinessLogic.Models.PrintingEditions;
using System;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public abstract Task<PrintingEditionModel> GetModelAsync(long id);
    }
}
