using AutoMapper;
using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public class BulkAccountRequestRepository : IBulkAccountRequestRepository
{
    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;
    private readonly IMapper _mapper;

    public BulkAccountRequestRepository(ILogger logger, IAccountOpeningMongoDBContext context, IMapper mapper)
    {
        this.logger = logger;
        this.context = context;
        _mapper = mapper;
    }

    public async Task<string> CreateBulkAccountRequest(BulkAccountRequest request)
    {

        await context.BulkAccountRequests.InsertOneAsync(request);
        return request.BulkAccountRequestId;

    }

    public async Task<List<BulkAccountRequest>> GetBulkAccountRequests()
    {
        var results = await context.BulkAccountRequests.FindAsync(_ => true);
        return results.ToList();

    }

    public async Task<BulkAccountRequest> GetBulkAccountRequest(string bulkAccountRequestId)
    {
        var filter = Builders<BulkAccountRequest>.Filter.Eq(m => m.BulkAccountRequestId, bulkAccountRequestId);
        var accountOpeningAttempt = await context.BulkAccountRequests.Find(filter).FirstOrDefaultAsync();

        return accountOpeningAttempt;

    }

    public async Task<bool> UpdateBulkAccountRequest(string id, BulkAccountRequest request)
    {
        var result = await context.BulkAccountRequests.ReplaceOneAsync(x => x.BulkAccountRequestId == id, request);
        return result.ModifiedCount == 1;
    }

    public async Task<List<BulkAccountRequest>> GetPendingBulkAccountRequests(string branchId)
    {
        var filter = Builders<BulkAccountRequest>.Filter.Eq(m => m.ApprovalStatus, ApprovalStatus.Pending) & 
            Builders<BulkAccountRequest>.Filter.Eq(x => x.BranchId,branchId);

        var bulkAccountRequests = await context.BulkAccountRequests.Find(filter).ToListAsync();

        return bulkAccountRequests;
    }

    // public async Task<PaginatedList<BulkAccountRequest>> GetAllAccountRequests(string branchId,int pageNumber = 1,int PageSize = 10)
    // {
    //     var filter = Builders<BulkAccountRequest>.Filter.Eq(x => x.BranchId, branchId);
    //     var bulk = await context.BulkAccountRequests.Find(filter).SortByDescending(x => x.DateCreated).ToListAsync();
        
    //     //var bulkRequests = await PaginatedList<BulkAccountDto>.CreateAsync(bulk.ProjectTo<BulkAccountDto>(_mapper.ConfigurationProvider), pageNumber, PageSize);
    //     return bulkRequests;

    // }
}