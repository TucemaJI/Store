using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<string> PrintingEditions { get; set; }
    }
}
