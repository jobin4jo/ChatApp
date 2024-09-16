
using System.Security.Cryptography;
using System.Text;
using ChatApp.Common.CommonDTO;
using ChatApp.DataTransferObjects;
using ChatApp.IRepositories;
using ChatApp.IService;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IPaymentServices paymentServices;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        public PaymentController(IPaymentRepository paymentRepository, IUserRepository userRepository, IPaymentServices paymentServices, IUserService userService)
        {
            this.paymentRepository = paymentRepository;
            this.userRepository = userRepository;
            this.paymentServices = paymentServices;
            this.userService = userService;
        }
        [HttpPost("Initial")]
        public async Task<IActionResult> InitiatePayment(PaymentClientRequestDTO paymentRequest)
        {

            Random rnd = new Random();
            var sender = await userService.GetUserByName(paymentRequest.senderName);

            var req = new PaymentRequestDTO
            {
                amount = paymentRequest.Amount,
                email = sender.EmailId,
                productInfo = "ChatAPP",
                firstName = sender.FirstName,
                Phonenumber = sender.phoneNumber,
                txnId = rnd.Next().ToString()
            };

            var response = await paymentRepository.InitiatePayment(req);
            return Ok(new { Code = 200, Status = true, Message = "", data = new { data = response, id = req.txnId } });
        }
        [HttpPost("callback")]
        public async Task<IActionResult> PaymentCallback([FromForm] PayUCallbackModel model)
        {
            var Sender = await userService.GetUserByName(model.firstname);
            var request = new PaymentServerRequestDTO
            {
                Amount = Convert.ToInt32(model.amount),
                paymentMode = model.mode,
                PaymentStatus = model.status,
                SenderId = Sender.Id,
                TransactionId = model.txnid,
                CreatedDate = DateTime.UtcNow

            };
            await paymentServices.UpdatePaymentDetail(request);


            if (model.status == "success")
            {
                return Redirect(Environment.GetEnvironmentVariable("PaymentSuccess"));
            }
            else
            {
                return Redirect(Environment.GetEnvironmentVariable("PaymentFailure"));
            }
        }
    }
}