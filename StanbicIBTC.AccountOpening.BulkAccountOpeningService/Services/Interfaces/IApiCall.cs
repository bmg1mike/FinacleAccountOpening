using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.BulkAccountOpeningService;

public interface IApiCall
{
    Task<List<BulkAccountRequest>> GetApprovedRequests();
    Task<string> OpenBulkAccounts(BulkAccountRequest requests);
}