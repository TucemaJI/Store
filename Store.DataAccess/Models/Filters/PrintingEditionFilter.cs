﻿using System.Collections.Generic;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Models.Filters
{
    public class PrintingEditionFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public CurrencyType Currency { get; set; }
        public List<PrintingEditionType> PrintingEditionType { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
