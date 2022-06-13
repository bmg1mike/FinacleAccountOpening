namespace StanbicIBTC.AccountOpening.Domain;

public class UploadHistoryDto
{
    public string BranchId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}