namespace ChatApp.DataTransferObjects;
public class PaymentServerRequestDTO
{
    public int SenderId { get; set; }
    public int RecieverId { get; set; }
    public int Amount { get; set; }
    public string paymentMode { get; set; }
    public string PaymentStatus { get; set; }
    public string TransactionId { get; set; }
    public DateTime CreatedDate { get; set; }
}