using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class PrintingEditionController : BaseController
    {
        private readonly PrintingEditionService _printingEditionService;
        public PrintingEditionController(PrintingEditionService printingEditionService, ILogger<PrintingEditionController> logger) : base(logger)
        {
            _printingEditionService = printingEditionService;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreatePrintingEdition")]
        public async Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            await _printingEditionService.CreatePrintingEditionAsync(model);
        }

        [HttpGet("GetPrintingEditions")]
        public async Task<List<PrintingEditionModel>> GetPrintingEditionModelsAsync()
        {
            return await _printingEditionService.GetPrintingEditionModelsAsync();
        }

        [HttpGet("GetPrintingEdition")]
        public async Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            return await _printingEditionService.GetPrintingEditionModelAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdatePrintingEdition")]
        public void UpdatePrintingEdition(PrintingEditionModel printingEditionModel)
        {
            _printingEditionService.UpdatePrintingEdition(printingEditionModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("DeletePrintingEdition")]
        public async Task DeletePrintingEditionAsync(long id)
        {
            await _printingEditionService.DeletePrintingEditionAsync(id);
        }
    }
}
