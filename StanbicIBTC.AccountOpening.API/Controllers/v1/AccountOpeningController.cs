using StanbicIBTC.AccountOpening.Data;
using StanbicIBTC.AccountOpening.Domain;
using StanbicIBTC.AccountOpening.Domain.Entities;
using StanbicIBTC.AccountOpening.Service;

namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

//[Authorize(AuthenticationSchemes = "Bearer")]
public class AccountOpeningController : BaseController
{
    private readonly IAccountOpeningService _accountOpeningService;

    public AccountOpeningController(IAccountOpeningService accountOpeningService)
    {
        _accountOpeningService = accountOpeningService;
    }

    [HttpPost("OpenTierOneAccount/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> OpenTierOneAccount(TierOneAccountOpeningRequest request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.ValidateTierOneAccountOpeningRequest(request);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("OpenTierThreeAccount/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> OpenTierThreeAccount(TierThreeAccountOpeningRequest request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.OpenTierThreeAccount(request);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }


    [HttpPost("OpenVirtualAccount/")]
    [ProducesResponseType(200, Type = typeof(Result<VirtualAccountOpeningResponse>))]
    public async Task<IActionResult> OpenVirtualAccount(CreateVirtualAccountDto request)
    {
        var result = new Result<VirtualAccountOpeningResponse>();
        var response = await _accountOpeningService.OpenVirtualAccount(request);
        result.Content = response;
        result.ResponseCode = response.ResponseCode;

        return Ok(result);
    }

    [HttpPost("AccountUpgrade/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> AccountUgrade(TierOneUgrade request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.UpgradeToTierThreeAccountOpeningRequest(request);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("OpenChessAccount/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> OpenChessAccount(ChessAccountRequest request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.OpenChessAccount(request);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("OpenCurrentAccount/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> OpenCurrentAccount(CurrentAccountRequest request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.OpenCurrentAccout(request);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetOccupations/")]
    [ProducesResponseType(200, Type = typeof(Result<List<OccupationResult>>))]
    public IActionResult GetOccupations()
    {
        var result = new Result<List<OccupationResult>>();
        var response = _accountOpeningService.GetOccupations();
        result.Content = response;
        result.ResponseCode = "000";
        return Ok(result);
    }


    [HttpGet("GetEmploymentStatus/")]
    [ProducesResponseType(200, Type = typeof(Result<List<EmploymentResult>>))]
    public IActionResult GetEmploymentStatus()
    {
        var result = new Result<List<EmploymentResult>>();
        var response = _accountOpeningService.GetEmploymentStatus();
        result.Content = response;
        result.ResponseCode = "000";
        return Ok(result);
    }

    [HttpGet("GetBranches/")]
    [ProducesResponseType(200, Type = typeof(Result<List<BranchesResponse>>))]
    public IActionResult GetBranches()
    {
        var result = new Result<List<BranchesResponse>>();
        var response = _accountOpeningService.GetBranches();
        result.Content = (List<BranchesResponse>)response.data;
        result.ResponseCode = "000";
        return Ok(result);
    }

    [HttpPost("GetAccountNameByAccountNumber/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public IActionResult GetAccountNameByAccountNumber(AccountVerificationDto request)
    {
        var result = new Result<string>();
        var response = _accountOpeningService.GetAccountNameByAccountNumber(request.AccountNumber);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        result.requestId = Guid.NewGuid().ToString();
        return Ok(result);
    }

    [HttpPost("GetAccountDetailsByBvn/")]
    public async Task<IActionResult> GetAccountDetailsByBvn([FromBody] GetAccoutDetailsByBvnDto request)
    {
        var response = await _accountOpeningService.GetAccountDetailsByBvn(request.Bvn);
        return Ok(response);
    }

    [HttpGet("GetBVNDetails/")]
    public async Task<IActionResult> GetBVNDetails(string bvn)
    {
        var bvnResponse = await _accountOpeningService.GetBVNDetails(bvn);
        return Ok(new
        {
            responseCode = bvnResponse.responseCode,
            responseDescription = bvnResponse.responseDescription,
            Data = bvnResponse.data
        });
    }

    [HttpPost("VerifyIdCard/")]
    public async Task<IActionResult> VerifyIdCard([FromBody] IdVerificationRequest request)
    {
        var result = new Result<IdVerificationResponse>();
        var response = await _accountOpeningService.VerifyIdCard(request);
        result.Content = (IdVerificationResponse)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("CheckAccountAvailabilityByBvn/{bvn}")]
    public async Task<IActionResult> CheckAccountAvailabilityByBvn(string bvn)
    {
        var result = new Result<CifCheck>();
        var response = await _accountOpeningService.CheckAccountAvailabilityByBvn(bvn);
        result.Content = (CifCheck)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetFailedAccountsByAccountManager/{sapId}")]
    [ProducesResponseType(200, Type = typeof(Result<List<UserResponse>>))]
    public async Task<IActionResult> GetFailedAccountsByAccountManager(string sapId)
    {
        var result = new Result<List<UserResponse>>();
        var response = await _accountOpeningService.GetFailedCifRequestsByAccountManager(sapId);
        result.Content = (List<UserResponse>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetSuccessfulAccountsByAccountManager/{sapId}")]
    [ProducesResponseType(200, Type = typeof(Result<List<UserResponse>>))]
    public async Task<IActionResult> GetSuccessfulAccountsByAccountManager(string sapId)
    {
        var result = new Result<List<UserResponse>>();
        var response = await _accountOpeningService.GetSuccessfulCifRequestsByAccountManager(sapId);
        result.Content = (List<UserResponse>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetPendingAccountsByAccountManager/{sapId}")]
    [ProducesResponseType(200, Type = typeof(Result<List<UserResponse>>))]
    public async Task<IActionResult> GetPendingAccountsByAccountManager(string sapId)
    {
        var result = new Result<List<UserResponse>>();
        var response = await _accountOpeningService.GetPendingCifRequestsByAccountManager(sapId);
        result.Content = (List<UserResponse>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("ApproveSanctionScreeningRequest/{id}")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> ApproveSanctionScreeningRequest(string id, [FromBody] SanctionScreeningComplianceRequest request)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.ApproveFailedSanctionScreeningReport(request, id);
        result.Content = response.responseDescription;
        return Ok(result);
    }

    [HttpGet("GetFailedSanctionScreenRequests")]
    [ProducesResponseType(200, Type = typeof(Result<List<SanctionScreeningDto>>))]
    public IActionResult GetFailedSanctionScreenRequests()
    {
        var result = new Result<List<SanctionScreeningDto>>();
        var response = _accountOpeningService.GetFailedSanctionScreenRequests();
        result.Content = (List<SanctionScreeningDto>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetRmIdentities")]
    [ProducesResponseType(200, Type = typeof(Result<List<RmIdentityDto>>))]
    public async Task<IActionResult> GetRmIdentities()
    {

        var result = new Result<List<RmIdentityDto>>();
        var response = await _accountOpeningService.GetRMIdentities();
        result.Content = (List<RmIdentityDto>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("AddRelationshipManager")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> AddRelationshipManager(RmIdentityDto data)
    {
        var result = new Result<string>();
        var response = await _accountOpeningService.AddRelationshipManager(data);
        result.Content = (string)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetAccountOpeningDetails")]
    [ProducesResponseType(200, Type = typeof(Result<List<AccountOpeningDetails>>))]
    public async Task<IActionResult> GetAccountOpeningDetails()
    {
        var result = new Result<List<AccountOpeningDetails>>();
        var response = await _accountOpeningService.GetAccountOpeningDetails();
        result.Content = (List<AccountOpeningDetails>)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetAccountOpeningDetails/{id}")]
    [ProducesResponseType(200, Type = typeof(AccountOpeningDetails))]
    public async Task<IActionResult> GetAccountOpeningDetailsById(string id)
    {
        var result = new Result<AccountOpeningDetails>();
        var response = await _accountOpeningService.GetAccountDetailsById(id);
        result.Content = (AccountOpeningDetails)response.data;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

}