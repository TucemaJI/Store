using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Areas.Admin.Pages
{
    public class MainPageModel : PageModel
    {
        private IAccountService _accountService;
        public MainPageModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAsync()
        {
            var accessToken = HttpContext.Request.Cookies[AdminConsts.ACCESS_TOKEN];

            await _accountService.SignOutAsync(accessToken);

            HttpContext.Response.Cookies.Delete(AdminConsts.ACCESS_TOKEN);
            HttpContext.Response.Cookies.Delete(AdminConsts.REFRESH_TOKEN);
        }
    }
}
