using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Areas.Admin.Pages
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class PrintingEditionsPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
