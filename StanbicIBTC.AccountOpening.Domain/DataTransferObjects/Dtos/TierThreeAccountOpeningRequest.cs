namespace StanbicIBTC.AccountOpening.Domain;

public class TierThreeAccountOpeningRequest
{
    public string Bvn { get; set; }
    public decimal  MonthlyIncome { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public string IdentityType { get; set; }
    public string IdNumber { get; set; }
    public string IdImage { get; set; }
    public DateTime IdIssueDate { get; set; }
    public DateTime? IdExpiryDate { get; set; }
    public NextOfKin NextOfKinDetails { get; set; }
}
