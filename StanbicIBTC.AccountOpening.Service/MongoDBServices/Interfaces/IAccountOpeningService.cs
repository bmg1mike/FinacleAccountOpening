namespace StanbicIBTC.AccountOpening.Service;
public interface IAccountOpeningService
{
    Task<AccountOpeningModel> OpenAccount(CIFRequest request);
    Task<string> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request);
}