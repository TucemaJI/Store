using System.Collections.Generic;

namespace Store.Presentation.Models
{
    public class FilterViewModel<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; }
    }
}
