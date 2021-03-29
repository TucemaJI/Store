using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Services.Interfaces;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Areas.Admin.Pages
{
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public SignInModel SignInModel { get; set; }
        private IAccountService _accountService;
        public LoginPageModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<RedirectToPageResult> OnPostAsync()
        {
            var result = await _accountService.SignInAsync(SignInModel);
            HttpContext.Response.Cookies.Append(AdminConsts.ACCESS_TOKEN, result.AccessToken);
            HttpContext.Response.Cookies.Append(AdminConsts.REFRESH_TOKEN, result.RefreshToken);
            return RedirectToPage("MainPage");
        }
    }
}
