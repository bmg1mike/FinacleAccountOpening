namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneUgrade
{
    public string AccountNumber { get; set; }
    public decimal  MonthlyIncome { get; set; }
    public string Bvn { get; set; }
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public string IdentityType { get; set; }
    public string  PassportPhotograph { get; set; }
    public string IdNumber { get; set; }
    public string IdImage { get; set; }
    public DateTime IdIssueDate { get; set; }
    public DateTime? IdExpiryDate { get; set; }
    public NextOfKin NextOfKin { get; set; }
    public string UtilityBill { get; set; }
    public string Signature { get; set; }
    public Platform Platform { get; set; }

}

