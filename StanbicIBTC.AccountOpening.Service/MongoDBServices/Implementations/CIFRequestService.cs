namespace StanbicIBTC.AccountOpening.Core.Services;
public partial class CIFRequestService : ICIFRequestService
{
    private readonly ICIFRequestRepository cIFRequestRepository;
    private readonly ILogger<CIFRequestService> logger;


    public CIFRequestService(ICIFRequestRepository cIFRequestRepository, ILogger<CIFRequestService> logger)
    {
        this.cIFRequestRepository = cIFRequestRepository;
        this.logger = logger;

    }

    public async Task<string> CreateCIFRequest(CIFRequest cIFRequest)
    {
        try
        {
           return await cIFRequestRepository.CreateCIFRequest(cIFRequest);
        }
        catch(Exception ex)
        {           
            logger.LogError(ex,"Error while creating CIFRequest");
            return "";
        }
    }

    public async Task<List<CIFRequest>> GetCIFRequests()
    {
        try
        {
          var results = await cIFRequestRepository.GetCIFRequests();
          return results.ToList();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFRequests");

            return null;
        }
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your ICIFRequestService.cs
    /* public async Task<List<CIFRequest>> GetByFieldName(string fieldName)
    {
        try
        {
         var results = await cIFRequestRepository.GetByFieldNameCIFRequests();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFRequests");

            return null;  
        }
    } */

    public async Task<CIFRequest> GetCIFRequest(string cIFRequestId)
    {
        try
        {
            var results = await cIFRequestRepository.GetCIFRequest(cIFRequestId);
            return results;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFRequest");

            return null;  
        }
    }

    public async Task<bool> RemoveCIFRequest(string cIFRequestId)
    {
        try
        {
            return await cIFRequestRepository.RemoveCIFRequest(cIFRequestId);
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while removing CIFRequest");

            return false;  
        }
    }

    public async Task<bool> UpdateCIFRequest(string cIFRequestId, CIFRequest cIFRequest)
    {
        try
        {
          return await cIFRequestRepository.UpdateCIFRequest(cIFRequestId,cIFRequest); 
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while Updating CIFRequest");

            return false;  
        }   
    }

    //Update Specific Fields Template. Uncomment If Needed. Remember to add to your ICIFRequestService.cs

    /* public async Task<bool> UpdateSpecificFields(string cIFRequestId, CIFRequest cIFRequest)
    {
        try
        {
            var filter = Builders<CIFRequest>.Filter.Eq(m => m.CIFRequestId, cIFRequestId);
            var update = Builders<CIFRequest>.Update
            .Set(m => m.Field1, cIFRequest.Field1)
            .Set(m => m.Field2, cIFRequest.Field2)
            .Set(m => m.Field3, cIFRequest.Field3);
        
            var result = await context.CIFRequests.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while updating CIFRequest");

            return false; 
        }
    } */
}




