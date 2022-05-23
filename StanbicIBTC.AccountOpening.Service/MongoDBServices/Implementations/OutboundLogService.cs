namespace StanbicIBTC.AccountOpening.Core.Services;
public partial class OutboundLogService : IOutboundLogService
{
    private readonly IOutboundLogRepository outboundLogRepository;
    private readonly ILogger<OutboundLogService> logger;


    public OutboundLogService(IOutboundLogRepository outboundLogRepository, ILogger<OutboundLogService> logger)
    {
        this.outboundLogRepository = outboundLogRepository;
        this.logger = logger;

    }

    public async Task<string> CreateOutboundLog(OutboundLog outboundLog)
    {
        try
        {
           return await outboundLogRepository.CreateOutboundLog(outboundLog);
        }
        catch(Exception ex)
        {           
            logger.LogError(ex,"Error while creating OutboundLog");
            return "";
        }
    }

    public async Task<List<OutboundLog>> GetOutboundLogs()
    {
        try
        {
          var results = await outboundLogRepository.GetOutboundLogs();
          return results.ToList();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving OutboundLogs");

            return null;
        }
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your IOutboundLogService.cs
    /* public async Task<List<OutboundLog>> GetByFieldName(string fieldName)
    {
        try
        {
         var results = await outboundLogRepository.GetByFieldNameOutboundLogs();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving OutboundLogs");

            return null;  
        }
    } */

    public async Task<OutboundLog> GetOutboundLog(string outboundLogId)
    {
        try
        {
            var results = await outboundLogRepository.GetOutboundLog(outboundLogId);
            return results;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving OutboundLog");

            return null;  
        }
    }

    public async Task<bool> RemoveOutboundLog(string outboundLogId)
    {
        try
        {
            return await outboundLogRepository.RemoveOutboundLog(outboundLogId);
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while removing OutboundLog");

            return false;  
        }
    }

    public async Task<bool> UpdateOutboundLog(string outboundLogId, OutboundLog outboundLog)
    {
        try
        {
          return await outboundLogRepository.UpdateOutboundLog(outboundLogId,outboundLog); 
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while Updating OutboundLog");

            return false;  
        }   
    }

    //Update Specific Fields Template. Uncomment If Needed. Remember to add to your IOutboundLogService.cs

    /* public async Task<bool> UpdateSpecificFields(string outboundLogId, OutboundLog outboundLog)
    {
        try
        {
            var filter = Builders<OutboundLog>.Filter.Eq(m => m.OutboundLogId, outboundLogId);
            var update = Builders<OutboundLog>.Update
            .Set(m => m.Field1, outboundLog.Field1)
            .Set(m => m.Field2, outboundLog.Field2)
            .Set(m => m.Field3, outboundLog.Field3);
        
            var result = await context.OutboundLogs.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while updating OutboundLog");

            return false; 
        }
    } */
}




