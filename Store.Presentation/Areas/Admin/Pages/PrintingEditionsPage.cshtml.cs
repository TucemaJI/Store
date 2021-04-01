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
using static Store.Shared.Constants.Constants;
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
            PrintingEditionFilter.PrintingEditionTypeList = new List<PrintingEditionType> { PrintingEditionType.None };
            await GetPageAsync();
        }

        public async Task<PageResult> OnPostAsync(string orderByString, int? pageIndex)
        {
            if (pageIndex is not null)
            {
                PrintingEditionFilter.PageOptions.CurrentPage = (int)pageIndex;
            }
            if (!string.IsNullOrWhiteSpace(orderByString))
            {
                PrintingEditionFilter.OrderByString = orderByString;
            }
            await GetPageAsync();
            return Page();

        }

        public async Task<RedirectToPageResult> OnPostDeleteAsync(long id)
        {
            await _printingEditionService.DeletePrintingEditionAsync(id);
            return RedirectToPage(PathConsts.PRINTING_EDITIONS_PAGE);
        }

        private async Task GetPageAsync()
        {
            PrintingEditionFilter.MinPrice = 0;
            PrintingEditionFilter.MaxPrice = int.MaxValue;
            var printingEditionList = await _printingEditionService.GetPrintingEditionModelListAsync(PrintingEditionFilter);
            PrintingEditionList = printingEditionList.Elements;
            PrintingEditionFilter.PageOptions = printingEditionList.PageOptions;
        }
    }
}
