using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Store.Shared.Enums.Enums;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public CurrencyType Currency { get; set; }
        [Required]
        public PrintingEditionType Type { get; set; }
        [Required]
        public List<long> AuthorsIdList { get; set; }
        public List<string> Authors { get; set; }

        public PrintingEditionModel()
        {
            Authors = new List<string>();
            AuthorsIdList = new List<long>();
        }
    }
}
