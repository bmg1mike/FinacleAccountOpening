namespace StanbicIBTC.AccountOpening.Core.Services;
public partial class AccountOpeningAttemptService : IAccountOpeningAttemptService
{
    private readonly IAccountOpeningAttemptRepository accountOpeningAttemptRepository;
    private readonly ILogger logger;


    public AccountOpeningAttemptService(IAccountOpeningAttemptRepository accountOpeningAttemptRepository, ILogger logger)
    {
        this.accountOpeningAttemptRepository = accountOpeningAttemptRepository;
        this.logger = logger;

    }

    public async Task<string> CreateAccountOpeningAttempt(AccountOpeningAttempt accountOpeningAttempt)
    {
        try
        {
           return await accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
        }
        catch(Exception ex)
        {           
            logger.Error(ex,"Error while creating AccountOpeningAttempt");
            return "";
        }
    }

    public async Task<List<AccountOpeningAttempt>> GetAccountOpeningAttempts()
    {
        try
        {
          var results = await accountOpeningAttemptRepository.GetAccountOpeningAttempts();
          return results.ToList();
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while retrieving AccountOpeningAttempts");

            return null;
        }
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your IAccountOpeningAttemptService.cs
    /* public async Task<List<AccountOpeningAttempt>> GetByFieldName(string fieldName)
    {
        try
        {
         var results = await accountOpeningAttemptRepository.GetByFieldNameAccountOpeningAttempts();
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while retrieving AccountOpeningAttempts");

            return null;  
        }
    } */

    public async Task<AccountOpeningAttempt> GetAccountOpeningAttempt(string accountOpeningAttemptId)
    {
        try
        {
            var results = await accountOpeningAttemptRepository.GetAccountOpeningAttempt(accountOpeningAttemptId);
            return results;
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while retrieving AccountOpeningAttempt");

            return null;  
        }
    }

    public async Task<bool> RemoveAccountOpeningAttempt(string accountOpeningAttemptId)
    {
        try
        {
            return await accountOpeningAttemptRepository.RemoveAccountOpeningAttempt(accountOpeningAttemptId);
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while removing AccountOpeningAttempt");

            return false;  
        }
    }

    public async Task<bool> UpdateAccountOpeningAttempt(string accountOpeningAttemptId, AccountOpeningAttempt accountOpeningAttempt)
    {
        try
        {
          return await accountOpeningAttemptRepository.UpdateAccountOpeningAttempt(accountOpeningAttemptId,accountOpeningAttempt); 
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while Updating AccountOpeningAttempt");

            return false;  
        }   
    }

    //Update Specific Fields Template. Uncomment If Needed. Remember to add to your IAccountOpeningAttemptService.cs

    /* public async Task<bool> UpdateSpecificFields(string accountOpeningAttemptId, AccountOpeningAttempt accountOpeningAttempt)
    {
        try
        {
            var filter = Builders<AccountOpeningAttempt>.Filter.Eq(m => m.AccountOpeningAttemptId, accountOpeningAttemptId);
            var update = Builders<AccountOpeningAttempt>.Update
            .Set(m => m.Field1, accountOpeningAttempt.Field1)
            .Set(m => m.Field2, accountOpeningAttempt.Field2)
            .Set(m => m.Field3, accountOpeningAttempt.Field3);
        
            var result = await context.AccountOpeningAttempts.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
        catch(Exception ex)
        {
            logger.Error(ex,"Error while updating AccountOpeningAttempt");

            return false; 
        }
    } */
}




