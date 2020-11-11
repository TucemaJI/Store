using System;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Base
{
    public class BaseModel
    {
        public ICollection<string> Errors { get; set; }
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }

        public BaseModel()
        {
            Errors = new List<string>();
        }
    }
}
