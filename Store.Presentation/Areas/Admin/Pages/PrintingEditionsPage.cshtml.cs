using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class PrintingEditionsPageModel : PageModel
    {
        private readonly IPrintingEditionService _printingEditionService;
        public List<PrintingEditionModel> PrintingEditionList { get; set; }
        [BindProperty]
        public PrintingEditionFilter PrintingEditionFilter { get; set; }
        public PrintingEditionsPageModel(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        public async Task OnGetAsync()
        {
            PrintingEditionFilter = new PrintingEditionFilter();
            PrintingEditionFilter.PageOptions = new PageOptions();
            PrintingEditionFilter.MinPrice = 0;
            PrintingEditionFilter.MaxPrice = int.MaxValue;
            PrintingEditionFilter.PrintingEditionTypeList = new List<PrintingEditionType> { PrintingEditionType.None };
            var printingEditionList = await _printingEditionService.GetPrintingEditionModelListAsync(PrintingEditionFilter);
            PrintingEditionList = printingEditionList.Elements;
            PrintingEditionFilter.PageOptions = printingEditionList.PageOptions;
        }

        public async Task OnPostDeleteAsync(long id)
        {
            await _printingEditionService.DeletePrintingEditionAsync(id);
        }
    }
}
