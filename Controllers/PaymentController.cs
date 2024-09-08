
using ChatApp.DataTransferObjects;
using ChatApp.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }
        [HttpPost("Payment")]
        public async Task<IActionResult> InitiatePayment(PaymentRequestDTO paymentRequest)
        {
            try
            {
                var response = await paymentRepository.InitiatePayment(paymentRequest);
                return Ok(new { Code = 200, Status = true, Message = "PAYMENT_PROCESS_SUCCESS", Data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 400, Status = false, Message = "Payment process Failure", Data = ex.Message });
            }
        }
    }
}