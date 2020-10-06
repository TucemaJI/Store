using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Presentation.Models
{
    public class FilterViewModel<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; }
        //public PageOptions PageViewModel { get; set; }
    }
}
