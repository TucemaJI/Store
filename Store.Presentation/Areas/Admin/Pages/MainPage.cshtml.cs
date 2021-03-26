using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.BusinessLogic.Services.Interfaces;

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
        public async Task OnPost()
        {
            
        }
    }
}
