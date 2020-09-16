using Store.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public long PaymentId { get; set; }
    }
}
