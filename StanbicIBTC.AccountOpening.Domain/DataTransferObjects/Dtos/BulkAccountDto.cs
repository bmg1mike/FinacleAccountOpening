namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountDto
{
    public string BulkAccountRequestId { get; set; }
    public string File { get; set; }
    public string CreatedBy { get; set; }
    public string ApprovedBy { get; set; }
    public string BranchId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}