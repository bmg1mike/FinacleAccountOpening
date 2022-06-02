using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public class BulkAccountRequestRepository : IBulkAccountRequestRepository
{
    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;

    public BulkAccountRequestRepository(ILogger logger, IAccountOpeningMongoDBContext context)
    {
        this.logger = logger;
        this.context = context;
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
}