namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountRequestDto
{
    public IFormFile File { get; set; }
    public string CreatedBy { get; set; }
    public string BranchId { get; set; }
}