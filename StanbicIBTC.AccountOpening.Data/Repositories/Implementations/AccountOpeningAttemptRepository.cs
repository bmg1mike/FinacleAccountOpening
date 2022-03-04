namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial class AccountOpeningAttemptRepository : IAccountOpeningAttemptRepository
{

    private readonly IAccountOpeningMongoDBContext context;
    private readonly ILogger logger;



    public AccountOpeningAttemptRepository(IAccountOpeningMongoDBContext context, ILogger logger)
    {
        this.context  = context;
        this.logger = logger;
    }

    public async Task<string> CreateAccountOpeningAttempt(AccountOpeningAttempt accountOpeningAttempt)
    {

        await  context.AccountOpeningAttempts.InsertOneAsync(accountOpeningAttempt);
        return accountOpeningAttempt.AccountOpeningAttemptId;

    }

    public async Task<List<AccountOpeningAttempt>> GetAccountOpeningAttempts()
    {
        var results = await context.AccountOpeningAttempts.FindAsync(_ => true);
        return results.ToList();
     
    }

    public async Task<AccountOpeningAttempt> GetAccountOpeningAttempt(string accountOpeningAttemptId)
    {
        var filter = Builders<AccountOpeningAttempt>.Filter.Eq(m => m.AccountOpeningAttemptId, accountOpeningAttemptId);
        var accountOpeningAttempt =await context.AccountOpeningAttempts.Find(filter).FirstOrDefaultAsync();

        return accountOpeningAttempt;
  
    }

    //GetList by any Field Name template. Uncomment If Needed. Remember to add to your IAccountOpeningAttemptRepository.cs
    /* public async Task<List<AccountOpeningAttempt>> GetByFieldName(string fieldName)
    {
        var filter = Builders<AccountOpeningAttempt>.Filter.Eq(m => m.fieldName, fieldName);
        var accountOpeningAttempts =await context.AccountOpeningAttempts.Find(filter).ToList();

        return accountOpeningAttempts ;
    } */

    public async Task<bool> RemoveAccountOpeningAttempt(string accountOpeningAttemptId)
    {
        var filter = Builders<AccountOpeningAttempt>.Filter.Eq(m => m.AccountOpeningAttemptId, accountOpeningAttemptId);
        var result = await context.AccountOpeningAttempts.DeleteOneAsync(filter);
        
        return result.DeletedCount == 1;
    
    }

    public async Task<bool> UpdateAccountOpeningAttempt(string id, AccountOpeningAttempt accountOpeningAttempt)
    {
        var result = await context.AccountOpeningAttempts.ReplaceOneAsync(accountOpeningAttempt => accountOpeningAttempt.AccountOpeningAttemptId == id, accountOpeningAttempt); 
        return result.ModifiedCount == 1;
    }

    //Use Template To Update Specific Fields According to Needs. Remember to add to your IAccountOpeningAttemptRepository.cs
    /*
    public async Task<bool> UpdateSpecificFields(string accountOpeningAttemptId, AccountOpeningAttempt accountOpeningAttempt)
    {
        var filter = Builders<AccountOpeningAttempt>.Filter.Eq(m => m.AccountOpeningAttemptId, accountOpeningAttemptId);
        var update = Builders<AccountOpeningAttempt>.Update
        .Set(m => m.Field1, accountOpeningAttempt.Field1)
        .Set(m => m.Field2, accountOpeningAttempt.Field2)
        .Set(m => m.Field3, accountOpeningAttempt.Field3);
    
        var result = await context.AccountOpeningAttempts.UpdateOneAsync(filter, update);

        return result.ModifiedCount == 1;
    }
    */
}




