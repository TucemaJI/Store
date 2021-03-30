using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AuthorsPageModel : PageModel
    {
        private readonly IAuthorService _authorService;
        public List<AuthorModel> AuthorList { get; set; }
        [BindProperty]
        public AuthorFilter AuthorFilter { get; set; }
        public AuthorsPageModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task OnGetAsync()
        {
            AuthorFilter = new AuthorFilter();
            AuthorFilter.Name = String.Empty;
            AuthorFilter.PageOptions = new PageOptions();
            var authorList = await _authorService.GetAuthorModelListAsync(AuthorFilter);
            AuthorList = authorList.Elements;
            AuthorFilter.PageOptions = authorList.PageOptions;
        }
        public async Task<PageResult> OnPostAsync(int? pageIndex)
        {
            if(pageIndex is not null)
            {
                AuthorFilter.PageOptions.CurrentPage = (int)pageIndex;
            }
            if(AuthorFilter.Id != 0)
            {
                AuthorFilter.OrderByString = "Name";
            }
            if (!string.IsNullOrWhiteSpace(AuthorFilter.Name))
            {
                AuthorFilter.OrderByString = "Id";
            }
            var authorList = await _authorService.GetAuthorModelListAsync(AuthorFilter);
            AuthorList = authorList.Elements;
            AuthorFilter.PageOptions = authorList.PageOptions;
            return Page();

        }
        public async Task OnPostDeleteAsync(long id)
        {
            await _authorService.DeleteAuthorAsync(id);
        }
    }
}
