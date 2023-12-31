namespace StanbicIBTC.AccountOpening.Service;
public interface IAccountOpeningService
{
    Task<string> CheckAddressVerificationStatus(string addressVerificationId);
    Task<string> DownloadAddressVerificationReport(string addressVerificationId);
    ApiResult GetAccountNameByAccountNumber(string accountNumber);
    Task<(string responseCode, string responseDescription, VerifyBVNResponseModel data)> GetBVNDetails(string bvn);
    List<EmploymentResult> GetEmploymentStatus();
    Task<(string responseCode, string responseDescription, AccountNINResponseModel data)> GetNinDetails(string nin, string dob);
    List<OccupationResult> GetOccupations();
    Task<string> OpenAccount(CIFRequest request);
    Task<ApiResult> OpenChessAccount(ChessAccountRequest request);
    Task<ApiResult> OpenCurrentAccout(CurrentAccountRequest request);
    Task OpenDomiciliaryAccount();
    Task<ApiResult> OpenTierThreeAccount(TierThreeAccountOpeningRequest request);
    Task<VirtualAccountOpeningResponse> OpenVirtualAccount(CreateVirtualAccountDto request);
    Task<string> UpgradeToTierThree(CIFRequest request);
    Task<ApiResult> UpgradeToTierThreeAccountOpeningRequest(TierOneUgrade request);
    Task<ApiResult> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request);
    Task<ApiResult> GetAccountDetailsByBvn(string bvn);
    ApiResult GetBranches();
    Task<ApiResult> OpenTierOneAccountForEzCash(TierOneAccountOpeningRequest request);
    Task<ApiResult> VerifyIdCard(IdVerificationRequest request);
    Task<ApiResult> CheckAccountAvailabilityByBvn(string bvn);
    Task<ApiResult> GetFailedCifRequestsByAccountManager(string sapId);
    Task<ApiResult> GetSuccessfulCifRequestsByAccountManager(string sapId);
    Task<ApiResult> GetPendingCifRequestsByAccountManager(string sapId);
    Task<ApiResult> ApproveFailedSanctionScreeningReport(SanctionScreeningComplianceRequest request, string cifRequestId);
    ApiResult GetFailedSanctionScreenRequests();
    Task<ApiResult> GetRMIdentities();
    Task<ApiResult> AddRelationshipManager(RmIdentityDto request);
    Task<ApiResult> GetAccountDetailsById(string id);
    Task<ApiResult> GetAccountOpeningDetails();

}