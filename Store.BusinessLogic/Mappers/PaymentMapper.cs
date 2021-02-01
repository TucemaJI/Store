using Store.BusinessLogic.Models.Payments;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    public class PaymentMapper : BaseMapper<Payment, PaymentModel>
    {
        public override Payment Map(PaymentModel element)
        {
            return new Payment
            {
                IsRemoved = element.IsRemoved,
                TransactionId = element.TransactionId,
            };
        }

        public override PaymentModel Map(Payment element)
        {
            return new PaymentModel
            {
                IsRemoved = element.IsRemoved,
                TransactionId = element.TransactionId,
                CreationDate = element.CreationData,
            };
        }
    }
}
