namespace StanbicIBTC.AccountOpening.Service;

public interface IBulkAccountOpeningService
{
    Task<ApiResult> ApproveOrRejectFile(BulkAccountDto request);
    List<BulkAccount> ReadFromExcel(string filePath);
    ApiResult UploadFile(BulkAccountRequestDto request);
    Task<Result<List<BulkAccountDto>>> GetBulkAccountRequestsByBranchId(string branchId);
    Task<Result<List<BulkRecentActivities>>> GetSuccessfullyOpenedAccountByBranchId(string branchId);
}
