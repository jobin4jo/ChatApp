using ChatApp.DataTransferObjects;
using ChatApp.DB_Context;
using ChatApp.IService;
using ChatApp.Model.Payments;

namespace ChatApp.Service
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ChatDBContext context;
        public PaymentServices(ChatDBContext context)
        {
            this.context = context;
        }
        public async Task UpdatePaymentDetail(PaymentServerRequestDTO paymentServerRequest)
        {
            var req = new Payments
            {
                Amount = paymentServerRequest.Amount,
                paymentMode = paymentServerRequest.paymentMode,
                PaymentStatus = paymentServerRequest.PaymentStatus,
                TransactionId = paymentServerRequest.TransactionId,
                CreatedDate = DateTime.UtcNow,
                SenderId = paymentServerRequest.SenderId,
                RecieverId = paymentServerRequest.RecieverId
            };

            context.payments.Add(req);
            await context.SaveChangesAsync();
        }
    }
}