namespace StanbicIBTC.AccountOpening.Service;

public interface IBulkAccountOpeningService
{
    Task<ApiResult> ApproveOrRejectFile(BulkAccountDto request);
    List<BulkAccount> ReadFromExcel(string filePath);
    ApiResult UploadFile(BulkAccountRequestDto request);
    Task<Result<List<BulkAccountDto>>> GetBulkAccountRequestsByBranchId(string branchId);
    Task<Result<List<BulkRecentActivities>>> GetSuccessfullyOpenedAccountByBranchId(string branchId,string bulkAccountId);
    Task<Result<PaginatedList<BulkAccountDto>>> UploadHistory(UploadHistoryDto history);
    Task<string> OpenBulkAccounts(BulkAccountRequest request);
    Task<List<BulkAccountRequest>> GetApprovedRequests();
    Task<DownloadFileResponse> DownloadFile(string fileName);
    Task<Result<List<BulkAccountDto>>> GetApprovedBulkAccountRequestsByBranchId(string branchId);
}
