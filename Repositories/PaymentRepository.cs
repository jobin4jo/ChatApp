
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
            this._settings = settings.Value;
        }
        public async Task<string> InitiatePayment(PaymentRequestDTO paymentRequest)
        {
            try
            {
                var txnId = "TXN" + new Random().Next(1000000000);
                var hash = GenerateHash(txnId, paymentRequest.Amount.ToString());
                var client = new RestClient(_settings.BaseUrl);
                var request = new RestRequest("", Method.Post);
                request.AddParameter("key", _settings.Key);
                request.AddParameter("txnid", txnId);
                request.AddParameter("amount", paymentRequest.Amount);
                request.AddParameter("productinfo", "testproduct");
                request.AddParameter("firstname", "jobin");
                request.AddParameter("email", "sample@gmail.com");
                request.AddParameter("phone", "8525963520");
                request.AddParameter("surl", _settings.SuccessUrl);
                request.AddParameter("furl", _settings.FailureUrl);
                request.AddParameter("hash", hash);
                request.AddParameter("upi", "1");
                request.AddParameter("udf1", paymentRequest.UPIId);
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
            catch (Exception)
            {
                throw;
            }
        }
        //public async Task<string> InitiatePayment(PaymentRequestDTO paymentRequest)
        //{
        //    try
        //    {
        //        var txnId = Guid.NewGuid().ToString();
        //        var hash = GenerateHash(txnId, paymentRequest.Amount.ToString());

        //        var client = new RestClient(_settings.BaseUrl);
        //        var request = new RestRequest("", Method.Post);

        //        request.AddParameter("key", _settings.Key);
        //        request.AddParameter("txnid", txnId);
        //        request.AddParameter("amount", "1000.00"); // Ensure amount is in correct format
        //        request.AddParameter("productinfo", "testproduct");
        //        request.AddParameter("firstname", "jobin");
        //        request.AddParameter("email", "sample@gmail.com");
        //        request.AddParameter("phone", "8525963520");
        //        request.AddParameter("surl", _settings.SuccessUrl);
        //        request.AddParameter("furl", _settings.FailureUrl);
        //        request.AddParameter("hash", hash);

        //        // Optional parameters (ensure they are valid or remove if not needed)
        //        // request.AddParameter("upi", "1"); // Check if 'upi' is required for your transaction
        //        // request.AddParameter("udf1", paymentRequest.UPIId); // Check if 'udf1' is required

        //        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

        //        var response = await client.ExecuteAsync(request);

        //        if (response.IsSuccessful)
        //        {
        //            return response.ResponseUri.AbsoluteUri;
        //        }
        //        else
        //        {
        //            // Log or handle the error response
        //            return response.Content; // Consider logging the response.Content for debugging
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        throw new ApplicationException("Error initiating payment", ex);
        //    }
        //}
        private string GenerateHash(string txnId, string amount)
        {
            string product = "sample";
            string fname = "samplename";
            string email = "sample@gmail.com";
            // var hashString = $"{_settings.Key}|{txnId}|{amount}|||||||||||{_settings.Salt}";
            var hashString = $"{_settings.Key}|{txnId}|{amount}|{product}|{fname}|{email}|||||||||||{_settings.Salt}";
            using (var sha512 = new SHA512Managed())
            {
                var bytes = Encoding.UTF8.GetBytes(hashString);
                var hash = sha512.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }


}