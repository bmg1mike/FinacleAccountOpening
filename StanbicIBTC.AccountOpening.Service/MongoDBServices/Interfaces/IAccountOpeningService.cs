namespace StanbicIBTC.AccountOpening.Service;
public interface IAccountOpeningService
{
    Task<string> OpenAccount(CIFRequest request);
    Task<(string responseCode, string responseDescription)> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request);
    List<OccupationResult> GetOccupations();
    List<EmploymentResult> GetEmploymentStatus();
}