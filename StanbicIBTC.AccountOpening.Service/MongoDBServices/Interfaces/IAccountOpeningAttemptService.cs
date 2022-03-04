namespace StanbicIBTC.AccountOpening.Core.Services;

public partial interface IAccountOpeningAttemptService
{
    Task<List<AccountOpeningAttempt>> GetAccountOpeningAttempts();
    Task<AccountOpeningAttempt>  GetAccountOpeningAttempt(string id);
    Task<string> CreateAccountOpeningAttempt(AccountOpeningAttempt accountOpeningAttempt);
    Task<bool> UpdateAccountOpeningAttempt(string id, AccountOpeningAttempt accountOpeningAttempt);
    Task<bool> RemoveAccountOpeningAttempt(string id);
}
