namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountRequest
{
    public string Bvn { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountManager { get; set; }
    public string SolId { get; set; }
    public string FileName { get; set; }
    public string CreatedBy { get; set; }
    public string ApprovedBy { get; set; }
    public AccountOpeningStatus Status { get; set; } = AccountOpeningStatus.PendingApproval;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}