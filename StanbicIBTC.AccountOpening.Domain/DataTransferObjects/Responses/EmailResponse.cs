namespace StanbicIBTC.AccountOpening.Domain;

public class EmailResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public string path { get; set; }
    public bool success { get; set; }
    public string timestamp { get; set; }
}