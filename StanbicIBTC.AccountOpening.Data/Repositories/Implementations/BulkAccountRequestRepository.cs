using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
            Builders<BulkAccountRequest>.Filter.Eq(x => x.BranchId, branchId);


        var bulkAccountRequests = await context.BulkAccountRequests.Find(filter).ToListAsync();

        return bulkAccountRequests;
    }

    public async Task<List<BulkAccountRequest>> GetApprovedBulkAccountRequests(string branchId)
    {
        var filter = Builders<BulkAccountRequest>.Filter.Eq(m => m.ApprovalStatus, ApprovalStatus.Approved) &
            Builders<BulkAccountRequest>.Filter.Eq(x => x.BranchId, branchId);


        var bulkAccountRequests = await context.BulkAccountRequests.Find(filter).ToListAsync();

        return bulkAccountRequests;
    }

    public async Task<PaginatedList<BulkAccountDto>> GetAllAccountRequests(UploadHistoryDto history)
    {
        var bulk = context.BulkAccountRequests.AsQueryable()
            .Where(x => x.BranchId == history.BranchId)
            .OrderBy(x => x.DateCreated)
            .ProjectTo<BulkAccountDto>(_mapper.ConfigurationProvider);

        var bulkRequests = await PaginatedList<BulkAccountDto>.CreateAsync(bulk, history.PageNumber, history.PageSize);
        return bulkRequests;
    }

    public async Task<List<BulkAccountRequest>> GetApprovedRequests()
    {
        var filter = Builders<BulkAccountRequest>.Filter.Eq(m => m.ApprovalStatus, ApprovalStatus.Approved) &
              Builders<BulkAccountRequest>.Filter.Eq(x => x.IsTreated, false);

        var bulkAccountRequests = await context.BulkAccountRequests.Find(filter).ToListAsync();

        return bulkAccountRequests;
        //return requests;
    }
}