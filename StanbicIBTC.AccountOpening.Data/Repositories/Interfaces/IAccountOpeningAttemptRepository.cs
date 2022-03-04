namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial interface IAccountOpeningAttemptRepository
{
    Task<List<AccountOpeningAttempt>> GetAccountOpeningAttempts();
    Task<AccountOpeningAttempt>  GetAccountOpeningAttempt(string id);
    Task<string> CreateAccountOpeningAttempt(AccountOpeningAttempt accountOpeningAttempt);
    Task<bool> UpdateAccountOpeningAttempt(string id, AccountOpeningAttempt accountOpeningAttempt);
    Task<bool> RemoveAccountOpeningAttempt(string id);

    //Task<List<AccountOpeningAttempt>> GetByFieldName(string fieldName) --Template
    //Task<bool> UpdateSpecificFields(string accountOpeningAttemptId, AccountOpeningAttempt accountOpeningAttempt) --Template

}
