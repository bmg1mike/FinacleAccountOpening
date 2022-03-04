namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial class OutboundLogRepository : IOutboundLogRepository
{

    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;



    public OutboundLogRepository(IAccountOpeningMongoDBContext context, ILogger logger)
    {
        this.context  = context;
        this.logger = logger;
    }

    public async Task<string> CreateOutboundLog(OutboundLog outboundLog)
    {

        await  context.OutboundLogs.InsertOneAsync(outboundLog);
        return outboundLog.OutboundLogId;

    }

    public async Task<List<OutboundLog>> GetOutboundLogs()
    {
        var results = await context.OutboundLogs.FindAsync(_ => true);
        return results.ToList();
     
    }

    public async Task<OutboundLog> GetOutboundLog(string outboundLogId)
    {
        var filter = Builders<OutboundLog>.Filter.Eq(m => m.OutboundLogId, outboundLogId);
        var outboundLog =await context.OutboundLogs.Find(filter).FirstOrDefaultAsync();

        return outboundLog;
  
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your IOutboundLogRepository.cs
    /* public async Task<List<OutboundLog>> GetByFieldName(string fieldName)
    {
        var filter = Builders<OutboundLog>.Filter.Eq(m => m.fieldName, fieldName);
        var outboundLogs =await context.OutboundLogs.Find(filter).ToList();

        return outboundLogs ;
    } */

    public async Task<bool> RemoveOutboundLog(string outboundLogId)
    {
        var filter = Builders<OutboundLog>.Filter.Eq(m => m.OutboundLogId, outboundLogId);
        var result = await context.OutboundLogs.DeleteOneAsync(filter);
        
        return result.DeletedCount == 1;
    
    }

    public async Task<bool> UpdateOutboundLog(string id, OutboundLog outboundLog)
    {
        var result = await context.OutboundLogs.ReplaceOneAsync(outboundLog => outboundLog.OutboundLogId == id, outboundLog); 
        return result.ModifiedCount == 1;
    }

    //Use Template To Update Specific Fields According to Needs. Remember to add to your IOutboundLogRepository.cs
    /*
    public async Task<bool> UpdateSpecificFields(string outboundLogId, OutboundLog outboundLog)
    {
        var filter = Builders<OutboundLog>.Filter.Eq(m => m.OutboundLogId, outboundLogId);
        var update = Builders<OutboundLog>.Update
        .Set(m => m.Field1, outboundLog.Field1)
        .Set(m => m.Field2, outboundLog.Field2)
        .Set(m => m.Field3, outboundLog.Field3);
    
        var result = await context.OutboundLogs.UpdateOneAsync(filter, update);

        return result.ModifiedCount == 1;
    }
    */
}




