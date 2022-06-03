using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public interface IBulkAccountRequestRepository
{
    Task<string> CreateBulkAccountRequest(BulkAccountRequest request);
    Task<BulkAccountRequest> GetBulkAccountRequest(string bulkAccountRequestId);
    Task<List<BulkAccountRequest>> GetBulkAccountRequests();
    Task<bool> UpdateBulkAccountRequest(string id, BulkAccountRequest request);
    Task<List<BulkAccountRequest>> GetPendingBulkAccountRequests(string branchId);
}