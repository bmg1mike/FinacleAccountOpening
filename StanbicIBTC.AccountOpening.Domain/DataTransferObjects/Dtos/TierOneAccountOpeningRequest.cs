namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneAccountOpeningRequest
{
    public string OccupationCode { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string Bvn { get; set; }
    public string Nin { get; set; }
    public string PhoneNumber { get; set; }
    public Platform Platform { get; set; } = Platform.WEB;
    public DateTime DateOfBirth { get; set; }
}
