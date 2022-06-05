namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccount
{
    public string Bvn { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; } // YYYY-MM-DD
    public string Category { get; set; }
    public string BranchManagerSapId { get; set; }
    public string SolId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;

}