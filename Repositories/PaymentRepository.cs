
using ChatApp.Common.CommonDTO;
using ChatApp.DataTransferObjects;
using ChatApp.IRepositories;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PayUSettings _settings;
        public PaymentRepository(IOptions<PayUSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<string> InitiatePayment(PaymentRequestDTO paymentRequest)
        {
            var hashString = $"{_settings.Key}|{paymentRequest.txnId}|{paymentRequest.amount}|{paymentRequest.productInfo}|{paymentRequest.firstName}|{paymentRequest.email}|||||||||||{_settings.Salt}";
            var hash = GenerateHash(hashString);
            var client = new RestClient(_settings.BaseUrl);
            var request = new RestRequest("", Method.Post);
            request.AddParameter("key", _settings.Key);
            request.AddParameter("txnid", paymentRequest.txnId);
            request.AddParameter("amount", paymentRequest.amount);
            request.AddParameter("productinfo", paymentRequest.productInfo);
            request.AddParameter("firstname", paymentRequest.firstName);
            request.AddParameter("email", paymentRequest.email);
            request.AddParameter("phone", paymentRequest.Phonenumber);
            request.AddParameter("surl", _settings.SuccessUrl);
            request.AddParameter("furl", _settings.FailureUrl);
            request.AddParameter("hash", hash);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response.ResponseUri.AbsoluteUri;
            }
            else
            {
                return response.ResponseUri.AbsoluteUri;
            }
        }
        public string GenerateHash(string text)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }


}