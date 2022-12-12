using AutoMapper;
using AutoMapper.QueryableExtensions;
using StanbicIBTC.AccountOpening.Data.common;
using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public class RMIdentityRepository : IRMIdentityRepository
{
    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;
    private readonly IMapper _mapper;
    public RMIdentityRepository(IAccountOpeningMongoDBContext context, ILogger logger, IMapper mapper)
    {
        this.context = context;
        this.logger = logger;
        _mapper = mapper;
    }

    public async Task<RMIdentity> GetRMIdentityByAASapIdAsync(string sapId)
    {
        var identity = context.RMIdentities.AsQueryable().Where(x => x.SAP == sapId).FirstOrDefault();
        await Task.CompletedTask;
        return identity;
    }

    public async Task<List<RmIdentityDto>> GetRMIdentitiesAsync()
    {
        var identity = context.RMIdentities.AsQueryable().ToList();
        var identitydto = new List<RmIdentityDto>();
        foreach (var item in identity)
        {
            identitydto.Add(_mapper.Map<RmIdentityDto>(item));
        }
        await Task.CompletedTask;
        return identitydto;
    }

    public async Task<string> AddRmIdentity(RMIdentity identity)
    {
        await context.RMIdentities.InsertOneAsync(identity);
        return identity.RMIdentityId;
    }
}