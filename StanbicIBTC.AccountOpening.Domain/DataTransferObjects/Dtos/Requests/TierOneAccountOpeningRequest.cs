namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneAccountOpeningRequest
{
    public string OccupationCode { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string Bvn { get; set; }
    public string Title { get; set; }
    public string StateOfResidence { get; set; }
    public string LgaOfResidence { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public Platform Platform { get; set; } = Platform.WEB;
    public DateTime DateOfBirth { get; set; }
    public bool WillOnboard { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string SecretQuestion { get; set; }
    public string SecretAnswer { get; set; }
    public string Email { get; set; }
    public RequiredDocuments Documents { get; set; }
    public string BranchId { get; set; }
    public NextOfKin NextOfKinDetails { get; set; }
    public string CustomerRelationshipManager { get; set; }
    public string AccountManager { get; set; }
}
