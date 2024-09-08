namespace ChatApp.Common.CommonDTO;

public class PayUSettings
{
    public string Key { get; set; }
    public string Salt { get; set; }
    public string BaseUrl { get; set; }
    public string SuccessUrl { get; set; }
    public string FailureUrl { get; set; }
}

