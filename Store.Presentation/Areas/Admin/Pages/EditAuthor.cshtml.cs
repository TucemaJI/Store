using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using static Store.Shared.Constants.Constants;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class EditAuthorModel : PageModel
    {
        private readonly IAuthorService _authorService;
        [BindProperty]
        public AuthorModel AuthorModel { get; set; }
        public string Title { get; set; }

        public EditAuthorModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public void OnGet()
        {
            Title = "Add";
        }
        public async Task OnGetEditAsync(long id)
        {
            Title = "Edit";
            AuthorModel = await _authorService.GetAuthorModelAsync(id);
        }

        public async Task<RedirectToPageResult> OnPostAsync()
        {
            if(AuthorModel.Id == 0)
            {
                await _authorService.CreateAuthorAsync(AuthorModel);
                return RedirectToPage(PathConsts.AUTHORS_PAGE);
            }
            await _authorService.UpdateAuthorAsync(AuthorModel);
            return RedirectToPage(PathConsts.AUTHORS_PAGE);
        }
    }
}
