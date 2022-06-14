namespace StanbicIBTC.AccountOpening.Domain;

public class SMSResponse
{
    public int status { get; set; }
    public bool success { get; set; }
    public string message { get; set; }
    public string path { get; set; }
    public string timestamp { get; set; }
}