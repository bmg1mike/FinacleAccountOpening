namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneAccountOpeningRequest
{
    public string OccupationCode { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string Bvn { get; set; }
    public string PhoneNumber { get; set; }
    public Platform Platform { get; set; } = Platform.WEB;
    public DateTime DateOfBirth { get; set; }
    public bool WillOnboard { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string SecretQuestion { get; set; }
    public string SecretAnswer { get; set; }
    public string Email { get; set; }
}
