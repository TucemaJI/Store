using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;

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
        
    }
}
