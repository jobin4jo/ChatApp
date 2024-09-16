using ChatApp.DataTransferObjects;

namespace ChatApp.IService
{
    public interface IPaymentServices
    {
        Task UpdatePaymentDetail(PaymentServerRequestDTO paymentServerRequest);
    }
}