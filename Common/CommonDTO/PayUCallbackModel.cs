namespace ChatApp.Common.CommonDTO;
public class PayUCallbackModel
{
    public string txnid { get; set; }
    public string status { get; set; }
    public decimal amount { get; set; }
    public string hash { get; set; }
    public string mode { get; set; }
    public string firstname { get; set; }
}
