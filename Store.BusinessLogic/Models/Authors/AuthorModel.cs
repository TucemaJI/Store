using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        public string Name { get; set; }
        public List<PrintingEditionModel> PrintingEditions { get; set; }

        public AuthorModel()
        {
            PrintingEditions = new List<PrintingEditionModel>();
        }
    }
}
