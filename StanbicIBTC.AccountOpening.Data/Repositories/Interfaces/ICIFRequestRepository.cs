namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial interface ICIFRequestRepository
{
    Task<List<CIFRequest>> GetCIFRequests();
    Task<CIFRequest>  GetCIFRequest(string id);
    Task<string> CreateCIFRequest(CIFRequest cIFRequest);
    Task<bool> UpdateCIFRequest(string id, CIFRequest cIFRequest);
    Task<bool> RemoveCIFRequest(string id);

    //Task<List<CIFRequest>> GetByFieldName(string fieldName) --Template
    //Task<bool> UpdateSpecificFields(string cIFRequestId, CIFRequest cIFRequest) --Template

}
