using Store.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class OrderItem : BaseEntity
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public long PrintingEditionId { get; set; }
        public long OrderId { get; set; }
        public long Count { get; set; }
    }
}
