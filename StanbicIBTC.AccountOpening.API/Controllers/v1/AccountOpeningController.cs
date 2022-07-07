using StanbicIBTC.AccountOpening.Domain;
using StanbicIBTC.AccountOpening.Service;

namespace StanbicIBTC.AccountOpening.API.Controllers.v1
{
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

        // [HttpPost("BulkTierOneAccountOpening/")]
        // [ProducesResponseType(200, Type = typeof(Result<List<string>>))]
        // public async Task<IActionResult> BulkTierOneAccountOpening(List<TierOneAccountOpeningRequest> requests)
        // {
        //     var result = new Result<List<string>>();
        //     var response = await _accountOpeningService.BulkTierOneAccountOpening(requests);
        //     result.Content = response.Select(x => x.responseDescription).ToList();
        //     return Ok(result);
        // }

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

        [HttpGet("GetAccountDetailsByBvn")]
        public async Task<IActionResult> GetAccountDetailsByBvn(string bvn)
        {
            var response = await _accountOpeningService.GetAccountDetailsByBvn(bvn);
            return Ok(response);
        }
    }
}