namespace StanbicIBTC.AccountOpening.Core.Services;

public partial interface ICIFRequestService
{
    Task<List<CIFRequest>> GetCIFRequests();
    Task<CIFRequest>  GetCIFRequest(string id);
    Task<string> CreateCIFRequest(CIFRequest cIFRequest);
    Task<bool> UpdateCIFRequest(string id, CIFRequest cIFRequest);
    Task<bool> RemoveCIFRequest(string id);
}
