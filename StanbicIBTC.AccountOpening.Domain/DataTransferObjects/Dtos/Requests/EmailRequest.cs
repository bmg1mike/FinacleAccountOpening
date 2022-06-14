namespace StanbicIBTC.AccountOpening.Domain;

public class EmailRequest
{
    public string body { get; set; }
    public string sender { get; set; }
    public string subject { get; set; }
    public string to { get; set; }
}