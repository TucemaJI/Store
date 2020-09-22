using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Base;
using Store.DataAccess.Entities;
using System.Collections;
using System.Collections.Generic;
using static Store.DataAccess.Enums.Enums;

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
