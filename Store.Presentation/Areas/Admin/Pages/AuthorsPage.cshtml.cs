using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;
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
            AuthorFilter.Name = string.Empty;
            AuthorFilter.PageOptions = new PageOptions();
            await GetPageAsync();
        }
        public async Task<PageResult> OnPostAsync(int? pageIndex)
        {
            if (pageIndex is not null)
            {
                AuthorFilter.PageOptions.CurrentPage = (int)pageIndex;
            }
            if (AuthorFilter.Id != default)
            {
                AuthorFilter.OrderByField = "Name";
            }
            if (!string.IsNullOrWhiteSpace(AuthorFilter.Name))
            {
                AuthorFilter.OrderByField = "Id";
            }
            await GetPageAsync();
            return Page();

        }
        public async Task<RedirectToPageResult> OnPostDeleteAsync(long id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return RedirectToPage(PathConsts.AUTHORS_PAGE);
        }
        private async Task GetPageAsync()
        {
            var authorList = await _authorService.GetAuthorModelListAsync(AuthorFilter);
            AuthorList = authorList.Elements;
            AuthorFilter.PageOptions = authorList.PageOptions;
        }
    }
}
