using Store.DataAccess.Entities.Base;

namespace Store.DataAccess.Entities
{
    public class Payment : BaseEntity
    {
        public long TransactionId { get; set; }
    }
}
