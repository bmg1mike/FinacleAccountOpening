using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;
public partial interface ICIFRequestRepository
{
    Task<List<CIFRequest>> GetCIFRequests();
    Task<CIFRequest>  GetCIFRequest(string id);
    Task<string> CreateCIFRequest(CIFRequest cIFRequest);
    Task<bool> UpdateCIFRequest(string id, CIFRequest cIFRequest);
    Task<bool> RemoveCIFRequest(string id);
    Task<List<CIFRequest>> GetPendingCifRequests();
    Task<List<CIFRequest>> GetSuccessfullyOpenedAccountsByBranchId(string branchId, string bulkAccountId);
    Task<CIFRequest> GetCIFRequestByBvn(string bvn);
    Task<List<UserResponse>> GetFailedCifRequestsByAccountManager(string sapId);
    Task<List<UserResponse>> GetSuccessfulCifRequestsByAccountManager(string sapId);
    Task<List<UserResponse>> GetPendingCifRequestsByAccountManager(string sapId);
    List<CIFRequest> GetFailedSanctionScreeningRequests();

    //Task<List<CIFRequest>> GetByFieldName(string fieldName) --Template
    //Task<bool> UpdateSpecificFields(string cIFRequestId, CIFRequest cIFRequest) --Template

}
