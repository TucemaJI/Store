using Store.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [Required]
        public string SubtitleReturned { get; set; }
        public CurrencyType ReturnedCurrency { get; set; }
        public PrintingEditionType Type { get; set; }
        public List<AuthorInPrintingEdition> AuthorsInPrintingEdition { get; set; }

        public PrintingEdition()
        {
            AuthorsInPrintingEdition = new List<AuthorInPrintingEdition>();
        }
    }
}
