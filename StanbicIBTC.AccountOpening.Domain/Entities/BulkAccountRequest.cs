namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountRequest
{
    public IFormFile File { get; set; }
    public string CreatedBy { get; set; }
    public string ApprovedBy { get; set; }
    public int MyProperty { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}
