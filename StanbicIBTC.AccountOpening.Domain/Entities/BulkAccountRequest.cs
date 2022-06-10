namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountRequest
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
    public string BulkAccountRequestId { get; set; }
    public string File { get; set; }
    public string CreatedBy { get; set; }
    public string ApprovedBy { get; set; }
    public string BranchId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
    public string Comment { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}
