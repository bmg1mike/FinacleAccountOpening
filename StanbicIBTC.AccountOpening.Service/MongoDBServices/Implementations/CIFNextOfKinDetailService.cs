namespace StanbicIBTC.AccountOpening.Core.Services;
public partial class CIFNextOfKinDetailService : ICIFNextOfKinDetailService
{
    private readonly ICIFNextOfKinDetailRepository cIFNextOfKinDetailRepository;
    private readonly ILogger<CIFNextOfKinDetailService> logger;


    public CIFNextOfKinDetailService(ICIFNextOfKinDetailRepository cIFNextOfKinDetailRepository, ILogger<CIFNextOfKinDetailService> logger)
    {
        this.cIFNextOfKinDetailRepository = cIFNextOfKinDetailRepository;
        this.logger = logger;

    }

    public async Task<string> CreateCIFNextOfKinDetail(CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        try
        {
           return await cIFNextOfKinDetailRepository.CreateCIFNextOfKinDetail(cIFNextOfKinDetail);
        }
        catch(Exception ex)
        {           
            logger.LogError(ex,"Error while creating CIFNextOfKinDetail");
            return "";
        }
    }

    public async Task<List<CIFNextOfKinDetail>> GetCIFNextOfKinDetails()
    {
        try
        {
          var results = await cIFNextOfKinDetailRepository.GetCIFNextOfKinDetails();
          return results.ToList();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFNextOfKinDetails");

            return null;
        }
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your ICIFNextOfKinDetailService.cs
    /* public async Task<List<CIFNextOfKinDetail>> GetByFieldName(string fieldName)
    {
        try
        {
         var results = await cIFNextOfKinDetailRepository.GetByFieldNameCIFNextOfKinDetails();
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFNextOfKinDetails");

            return null;  
        }
    } */

    public async Task<CIFNextOfKinDetail> GetCIFNextOfKinDetail(string cIFNextOfKinDetailId)
    {
        try
        {
            var results = await cIFNextOfKinDetailRepository.GetCIFNextOfKinDetail(cIFNextOfKinDetailId);
            return results;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while retrieving CIFNextOfKinDetail");

            return null;  
        }
    }

    public async Task<bool> RemoveCIFNextOfKinDetail(string cIFNextOfKinDetailId)
    {
        try
        {
            return await cIFNextOfKinDetailRepository.RemoveCIFNextOfKinDetail(cIFNextOfKinDetailId);
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while removing CIFNextOfKinDetail");

            return false;  
        }
    }

    public async Task<bool> UpdateCIFNextOfKinDetail(string cIFNextOfKinDetailId, CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        try
        {
          return await cIFNextOfKinDetailRepository.UpdateCIFNextOfKinDetail(cIFNextOfKinDetailId,cIFNextOfKinDetail); 
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while Updating CIFNextOfKinDetail");

            return false;  
        }   
    }

    //Update Specific Fields Template. Uncomment If Needed. Remember to add to your ICIFNextOfKinDetailService.cs

    /* public async Task<bool> UpdateSpecificFields(string cIFNextOfKinDetailId, CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        try
        {
            var filter = Builders<CIFNextOfKinDetail>.Filter.Eq(m => m.CIFNextOfKinDetailId, cIFNextOfKinDetailId);
            var update = Builders<CIFNextOfKinDetail>.Update
            .Set(m => m.Field1, cIFNextOfKinDetail.Field1)
            .Set(m => m.Field2, cIFNextOfKinDetail.Field2)
            .Set(m => m.Field3, cIFNextOfKinDetail.Field3);
        
            var result = await context.CIFNextOfKinDetails.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"Error while updating CIFNextOfKinDetail");

            return false; 
        }
    } */
}




