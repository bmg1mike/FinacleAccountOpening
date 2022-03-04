namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneAccountOpeningRequest
{
    public string OccupationCode { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string Bvn { get; set; }
    public string Nin { get; set; }
    public string MaritalStatus { get; set; }
    public string SecretWord { get; set; }
    public string PhoneNumber { get; set; }
    public Platform Platform { get; set; }
    public AccountOpeningStatus AccountOpeningStatus { get; set; }
    public DateTime DateOfBirth { get; set; }
}
