using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;
public partial class CIFRequestRepository : ICIFRequestRepository
{

    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;



    public CIFRequestRepository(IAccountOpeningMongoDBContext context, ILogger logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<string> CreateCIFRequest(CIFRequest cIFRequest)
    {

        await context.CIFRequests.InsertOneAsync(cIFRequest);
        return cIFRequest.CIFRequestId;

    }

    public async Task<List<CIFRequest>> GetCIFRequests()
    {
        var results = await context.CIFRequests.FindAsync(_ => true);
        return results.ToList();

    }

    public async Task<CIFRequest> GetCIFRequest(string cIFRequestId)
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.CIFRequestId, cIFRequestId);
        var cIFRequest = await context.CIFRequests.Find(filter).FirstOrDefaultAsync();

        return cIFRequest;

    }

    public async Task<CIFRequest> GetCIFRequestByBvn(string bvn)
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.CustomerBVN, bvn);
        var cIFRequest = await context.CIFRequests.Find(filter).FirstOrDefaultAsync();

        return cIFRequest;

    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your ICIFRequestRepository.cs
    /* public async Task<List<CIFRequest>> GetByFieldName(string fieldName)
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.fieldName, fieldName);
        var cIFRequests =await context.CIFRequests.Find(filter).ToList();

        return cIFRequests ;
    } */
    public async Task<List<CIFRequest>> GetSuccessfullyOpenedAccountsByBranchId(string branchId, string bulkAccountId)
    {
        var filter = context.CIFRequests.AsQueryable();
        var cifRequests = filter.Where(x => x.SolId == branchId && x.BulkAccountId == bulkAccountId && x.IsAccountOpenedSuccessfully == true)
                        .OrderByDescending(x => x.DateModified).ToList();
        return cifRequests;
    }
    public async Task<List<CIFRequest>> GetPendingCifRequests()
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.AccountOpeningStatus, AccountOpeningStatus.Pending.ToString());
        var cIFRequests = await context.CIFRequests.Find(filter).ToListAsync();

        return cIFRequests;
    }


    public async Task<bool> RemoveCIFRequest(string cIFRequestId)
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.CIFRequestId, cIFRequestId);
        var result = await context.CIFRequests.DeleteOneAsync(filter);

        return result.DeletedCount == 1;

    }

    public async Task<bool> UpdateCIFRequest(string id, CIFRequest cIFRequest)
    {
        var result = await context.CIFRequests.ReplaceOneAsync(cIFRequest => cIFRequest.CIFRequestId == id, cIFRequest);
        return result.ModifiedCount == 1;
    }

    //Use Template To Update Specific Fields According to Needs. Remember to add to your ICIFRequestRepository.cs
    /*
    public async Task<bool> UpdateSpecificFields(string cIFRequestId, CIFRequest cIFRequest)
    {
        var filter = Builders<CIFRequest>.Filter.Eq(m => m.CIFRequestId, cIFRequestId);
        var update = Builders<CIFRequest>.Update
        .Set(m => m.Field1, cIFRequest.Field1)
        .Set(m => m.Field2, cIFRequest.Field2)
        .Set(m => m.Field3, cIFRequest.Field3);
    
        var result = await context.CIFRequests.UpdateOneAsync(filter, update);

        return result.ModifiedCount == 1;
    }
    */
}




