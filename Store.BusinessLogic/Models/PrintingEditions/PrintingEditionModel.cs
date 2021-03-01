using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public StatusType Status { get; set; }
        public CurrencyType Currency { get; set; }
        public PrintingEditionType Type { get; set; }
        public List<string> Authors { get; set; }

        public PrintingEditionModel()
        {
            Authors = new List<string>();
        }
    }
}
