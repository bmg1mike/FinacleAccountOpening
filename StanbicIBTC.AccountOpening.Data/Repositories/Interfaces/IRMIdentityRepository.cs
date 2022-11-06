using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public interface IRMIdentityRepository
{
    Task<string> AddRmIdentity(RMIdentity identity);
    Task<List<RmIdentityDto>> GetRMIdentitiesAsync();
    Task<RMIdentity> GetRMIdentityByAASapIdAsync(string sapId);
}