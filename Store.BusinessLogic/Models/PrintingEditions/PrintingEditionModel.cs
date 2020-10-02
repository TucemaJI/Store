using Store.BusinessLogic.Models.Base;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public PrintingEditionType Type { get; set; }

    }
}
