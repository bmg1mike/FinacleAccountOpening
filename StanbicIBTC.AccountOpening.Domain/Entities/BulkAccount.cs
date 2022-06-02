namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccount
{
    public string Bvn { get; set; }
    public string PhoneNumber { get; set; }
    public string Nin { get; set; }
    public string Category { get; set; }
    public string AccountManager { get; set; }
    public string SolId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;
    public AccountOpeningStatus Status { get; set; } = AccountOpeningStatus.PendingApproval;
    
}