using Store.Shared.Options;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionPageModel : PageModel<PrintingEditionModel>
    {
        public PrintingEditionPageModel(List<PrintingEditionModel> pagedList, PageOptions parameters) : base(pagedList, parameters) { }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
    }
}
