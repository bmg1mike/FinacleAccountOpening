namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial class CIFNextOfKinDetailRepository : ICIFNextOfKinDetailRepository
{

    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;



    public CIFNextOfKinDetailRepository(IAccountOpeningMongoDBContext context, ILogger logger)
    {
        this.context  = context;
        this.logger = logger;
    }

    public async Task<string> CreateCIFNextOfKinDetail(CIFNextOfKinDetail cIFNextOfKinDetail)
    {

        await  context.CIFNextOfKinDetails.InsertOneAsync(cIFNextOfKinDetail);
        return cIFNextOfKinDetail.CIFNextOfKinDetailId;

    }

    public async Task<List<CIFNextOfKinDetail>> GetCIFNextOfKinDetails()
    {
        var results = await context.CIFNextOfKinDetails.FindAsync(_ => true);
        return results.ToList();
     
    }

    public async Task<CIFNextOfKinDetail> GetCIFNextOfKinDetail(string cIFNextOfKinDetailId)
    {
        var filter = Builders<CIFNextOfKinDetail>.Filter.Eq(m => m.CIFNextOfKinDetailId, cIFNextOfKinDetailId);
        var cIFNextOfKinDetail =await context.CIFNextOfKinDetails.Find(filter).FirstOrDefaultAsync();

        return cIFNextOfKinDetail;
  
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your ICIFNextOfKinDetailRepository.cs
    /* public async Task<List<CIFNextOfKinDetail>> GetByFieldName(string fieldName)
    {
        var filter = Builders<CIFNextOfKinDetail>.Filter.Eq(m => m.fieldName, fieldName);
        var cIFNextOfKinDetails =await context.CIFNextOfKinDetails.Find(filter).ToList();

        return cIFNextOfKinDetails ;
    } */

    public async Task<bool> RemoveCIFNextOfKinDetail(string cIFNextOfKinDetailId)
    {
        var filter = Builders<CIFNextOfKinDetail>.Filter.Eq(m => m.CIFNextOfKinDetailId, cIFNextOfKinDetailId);
        var result = await context.CIFNextOfKinDetails.DeleteOneAsync(filter);
        
        return result.DeletedCount == 1;
    
    }

    public async Task<bool> UpdateCIFNextOfKinDetail(string id, CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        var result = await context.CIFNextOfKinDetails.ReplaceOneAsync(cIFNextOfKinDetail => cIFNextOfKinDetail.CIFNextOfKinDetailId == id, cIFNextOfKinDetail); 
        return result.ModifiedCount == 1;
    }

    //Use Template To Update Specific Fields According to Needs. Remember to add to your ICIFNextOfKinDetailRepository.cs
    /*
    public async Task<bool> UpdateSpecificFields(string cIFNextOfKinDetailId, CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        var filter = Builders<CIFNextOfKinDetail>.Filter.Eq(m => m.CIFNextOfKinDetailId, cIFNextOfKinDetailId);
        var update = Builders<CIFNextOfKinDetail>.Update
        .Set(m => m.Field1, cIFNextOfKinDetail.Field1)
        .Set(m => m.Field2, cIFNextOfKinDetail.Field2)
        .Set(m => m.Field3, cIFNextOfKinDetail.Field3);
    
        var result = await context.CIFNextOfKinDetails.UpdateOneAsync(filter, update);

        return result.ModifiedCount == 1;
    }
    */
}




