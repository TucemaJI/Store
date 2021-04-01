using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Shared.Options;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class UsersPageModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty]
        public List<UserModel> UserList { get; set; }
        [BindProperty]
        public UserFilter UserFilter { get; set; }
        public UsersPageModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task OnGetAsync()
        {
            UserFilter = new UserFilter();
            UserFilter.PageOptions = new PageOptions();
            var userList = await _userService.FilterUsersAsync(UserFilter);
            UserList = userList.Elements;
            UserFilter.PageOptions = userList.PageOptions;
        }

        public async Task<PageResult> OnPostAsync(int? pageIndex)
        {
            if (pageIndex is not null)
            {
                UserFilter.PageOptions.CurrentPage = (int)pageIndex;
            }
            var userList = await _userService.FilterUsersAsync(UserFilter);
            UserList = userList.Elements;
            UserFilter.PageOptions = userList.PageOptions;
            return Page();

        }

        public async Task OnPostBlockAsync(string id)
        {
            await _userService.BlockUserAsync(id);
        }

        public async Task OnPostDeleteAsync(string id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
