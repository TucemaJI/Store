using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> PrintingEditions { get; set; }
    }
}
