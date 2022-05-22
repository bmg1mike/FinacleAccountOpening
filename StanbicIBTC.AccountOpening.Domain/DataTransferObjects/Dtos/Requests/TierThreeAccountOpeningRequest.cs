namespace StanbicIBTC.AccountOpening.Domain;

public class TierThreeAccountOpeningRequest
{
    public string Bvn { get; set; }
    // Contact Details
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ResidenceAddress { get; set; }
    public string NearestBusStop { get; set; }
    public string StateOfResidence { get; set; }
    // Employment Details
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public decimal MonthlyIncome { get; set; }
    public string CurrencyCode { get; set; } // For Domiciliary Accounts
    public string IdentityType { get; set; }
    public string IdNumber { get; set; }
    public string IdImage { get; set; } // regulatory Id
    public string PassportPhotograph { get; set; }
    public DateTime IdIssueDate { get; set; }
    public DateTime? IdExpiryDate { get; set; }
    public string TaxIdentificationNumber { get; set; }
    public string Nationality { get; set; }
    public NextOfKin NextOfKinDetails { get; set; }
    //If Nationality is not Nigerian
    public string ResidencePermitNumber { get; set; }
    public string PermitIssueDate { get; set; }
    public string PermitExpiryDate { get; set; }
    public Platform Platform { get; set; }
    public string EmploymentStatusCode { get; set; }
    public string OccupationCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Signature { get; set; }
    public string UtilityBill { get; set; }
}
