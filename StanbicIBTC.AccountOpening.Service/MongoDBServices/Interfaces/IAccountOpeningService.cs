namespace StanbicIBTC.AccountOpening.Service;
public interface IAccountOpeningService
{
    Task<string> OpenAccount(CIFRequest request);
    Task<ApiResult> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request);
    List<OccupationResult> GetOccupations();
    List<EmploymentResult> GetEmploymentStatus();
    Task<VirtualAccountOpeningResponse> OpenVirtualAccount(CreateVirtualAccountDto request);
    Task<List<ApiResult>> BulkTierOneAccountOpening(List<TierOneAccountOpeningRequest> requests);
}