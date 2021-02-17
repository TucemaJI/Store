using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class PrintingEditionController : BaseController
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService, ILogger<PrintingEditionController> logger) : base(logger)
        {
            _printingEditionService = printingEditionService;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreatePrintingEdition")]
        public Task CreatePrintingEditionAsync(PrintingEditionModel model)
        {
            return _printingEditionService.CreatePrintingEditionAsync(model);
        }

        [HttpPost("GetPrintingEditions")]
        public Task<List<PrintingEditionModel>> GetPrintingEditionModelsAsync([FromQuery] PrintingEditionFilter filter)
        {
            return _printingEditionService.GetPrintingEditionModelsAsync(filter);
        }

        [HttpGet("GetPrintingEdition")]
        public Task<PrintingEditionModel> GetPrintingEditionModelAsync(long id)
        {
            return _printingEditionService.GetPrintingEditionModelAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdatePrintingEdition")]
        public void UpdatePrintingEdition(PrintingEditionModel printingEditionModel)
        {
            _printingEditionService.UpdatePrintingEdition(printingEditionModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("DeletePrintingEdition")]
        public Task DeletePrintingEditionAsync(long id)
        {
            return _printingEditionService.DeletePrintingEditionAsync(id);
        }
    }
}
