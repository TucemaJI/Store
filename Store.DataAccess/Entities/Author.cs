using Store.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public List<PrintingEdition> PrintingEditions { get; set; }
    }
}
