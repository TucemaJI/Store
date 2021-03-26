using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Services.Interfaces;

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
            HttpContext.Response.Cookies.Append("accessKey", result.AccessToken);
            HttpContext.Response.Cookies.Append("refreshKey", result.RefreshToken);
            return RedirectToPage("MainPage");
        }
    }
}
