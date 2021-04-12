using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Controllers
{
    public class PrintingEditionController : BaseController
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreatePrintingEdition")]
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            await _printingEditionService.CreatePrintingEditionAsync(model);
        }

        [HttpPost("GetPrintingEditions")]
        public Task<PageModel<PrintingEditionModel>> GetPrintingEditionModelsAsync([FromBody] PrintingEditionFilter filter)
        {
            var result = _printingEditionService.GetPrintingEditionModelListAsync(filter);
            return result;
        }

        [HttpGet("{id:long}")]
        public Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            var result = _printingEditionService.GetPrintingEditionModelAsync(id);
            return result;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdatePrintingEdition")]
        public async Task UpdatePrintingEditionAsync(PrintingEditionModel printingEditionModel)
        {
            await _printingEditionService.UpdatePrintingEditionAsync(printingEditionModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("DeletePrintingEdition")]
        public async Task DeletePrintingEditionAsync(long id)
        {
            await _printingEditionService.DeletePrintingEditionAsync(id);
        }
    }
}
