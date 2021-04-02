using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Areas.Admin.Pages
{
    public class EditPrintingEditionModel : PageModel
    {
        private readonly IPrintingEditionService _printingEditionService;
        private readonly IAuthorService _authorService;
        [BindProperty]
        public PrintingEditionModel PrintingEditionModel { get; set; }
        public List<AuthorModel> Authors { get; set; }
        public string Title { get; set; }
        public EditPrintingEditionModel(IPrintingEditionService printingEditionService, IAuthorService authorService)
        {
            _printingEditionService = printingEditionService;
            _authorService = authorService;
        }
        public async Task OnGetAsync()
        {
            Title = "Add";
            Authors = await _authorService.GetAllAuthorModelListAsync();
        }
        public async Task OnGetEditAsync(long id)
        {
            Title = "Edit";
            PrintingEditionModel = await _printingEditionService.GetPrintingEditionModelAsync(id);
            Authors = await _authorService.GetAllAuthorModelListAsync();
        }
        public async Task<RedirectToPageResult> OnPostAsync()
        {
            if (PrintingEditionModel.Id == 0)
            {
                await _printingEditionService.CreatePrintingEditionAsync(PrintingEditionModel);
                return RedirectToPage(PathConsts.PRINTING_EDITIONS_PAGE);
            }
            await _printingEditionService.UpdatePrintingEdition(PrintingEditionModel);
            return RedirectToPage(PathConsts.PRINTING_EDITIONS_PAGE);
        }
    }
}
