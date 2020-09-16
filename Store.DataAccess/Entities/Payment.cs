using Store.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Payment : BaseEntity
    {
        public long TransactionId { get; set; }
    }
}
