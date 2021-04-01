using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Areas.Admin.Pages
{
    public class EditUserByAdminModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty]
        public UserModel UserModel { get; set; }

        public EditUserByAdminModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGetAsync(string id)
        {
            UserModel = await _userService.GetUserAsync(id);
        }


        public async Task<RedirectToPageResult> OnPostAsync()
        {
            await _userService.UpdateUserAsync(UserModel);
            return RedirectToPage(PathConsts.USERS_PAGE);
        }
    }
}
