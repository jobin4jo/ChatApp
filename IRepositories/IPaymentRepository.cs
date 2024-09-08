using ChatApp.DataTransferObjects;

namespace ChatApp.IRepositories;

public interface IPaymentRepository
{
    Task<string> InitiatePayment(PaymentRequestDTO paymentRequest);
}

