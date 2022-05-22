namespace StanbicIBTC.AccountOpening.Core.Services;
public partial class InboundLogService : IInboundLogService
{
    private readonly IInboundLogRepository inboundLogRepository;
    private readonly ILogger<InboundLogService> logger;


    public InboundLogService(IInboundLogRepository inboundLogRepository, ILogger<InboundLogService> logger)
    {
        this.inboundLogRepository = inboundLogRepository;
        this.logger = logger;

    }

    public async Task<string> CreateInboundLog(InboundLog inboundLog)
    {
        try
        {
           return await inboundLogRepository.CreateInboundLog(inboundLog);
        }
        catch(Exception ex)
        {           
            logger.LogError(ex,"Error while creating InboundLog");
            return "";
        }
    }

    public async Task<List<InboundLog>> GetInboundLogs()
    {
        try
        {
          var results = await inboundLogRepository.GetInboundLogs();
          return results.ToList();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving InboundLogs");

            return null;
        }
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your IInboundLogService.cs
    /* public async Task<List<InboundLog>> GetByFieldName(string fieldName)
    {
        try
        {
         var results = await inboundLogRepository.GetByFieldNameInboundLogs();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving InboundLogs");

            return null;  
        }
    } */

    public async Task<InboundLog> GetInboundLog(string inboundLogId)
    {
        try
        {
            var results = await inboundLogRepository.GetInboundLog(inboundLogId);
            return results;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving InboundLog");

            return null;  
        }
    }

    public async Task<bool> RemoveInboundLog(string inboundLogId)
    {
        try
        {
            return await inboundLogRepository.RemoveInboundLog(inboundLogId);
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while removing InboundLog");

            return false;  
        }
    }

    public async Task<bool> UpdateInboundLog(string inboundLogId, InboundLog inboundLog)
    {
        try
        {
          return await inboundLogRepository.UpdateInboundLog(inboundLogId,inboundLog); 
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while Updating InboundLog");

            return false;  
        }   
    }

    //Update Specific Fields Template. Uncomment If Needed. Remember to add to your IInboundLogService.cs

    /* public async Task<bool> UpdateSpecificFields(string inboundLogId, InboundLog inboundLog)
    {
        try
        {
            var filter = Builders<InboundLog>.Filter.Eq(m => m.InboundLogId, inboundLogId);
            var update = Builders<InboundLog>.Update
            .Set(m => m.Field1, inboundLog.Field1)
            .Set(m => m.Field2, inboundLog.Field2)
            .Set(m => m.Field3, inboundLog.Field3);
        
            var result = await context.InboundLogs.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while updating InboundLog");

            return false; 
        }
    } */
}




