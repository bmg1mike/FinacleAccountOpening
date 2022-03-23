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
        public async Task<IActionResult> OpenTierOneAccount(TierOneAccountOpeningRequest request)
        {
            var result = new Result<string>();
            var response = await _accountOpeningService.ValidateTierOneAccountOpeningRequest(request);
            result.Content = response.responseDescription;
            result.ResponseCode = response.responseCode;
            return Ok(result);
        }
        [HttpPost("BulkTierOneAccountOpening/")]
        public async Task<IActionResult> BulkTierOneAccountOpening(List<TierOneAccountOpeningRequest> requests)
        {
            var result = new Result<List<string>>();
            var response = await _accountOpeningService.BulkTierOneAccountOpening(requests);
            result.Content = response.Select(x => x.responseDescription).ToList();
            return Ok(result);
        }

        [HttpPost("OpenVirtualAccount/")]
        public async Task<IActionResult> OpenVirtualAccount(CreateVirtualAccountDto request)
        {
            var result = new Result<VirtualAccountOpeningResponse>();
            var response = await _accountOpeningService.OpenVirtualAccount(request);
            result.Content = response;
            result.ResponseCode = response.ResponseCode;

            return Ok(result.Content);
        }

        [HttpGet("GetOccupations/")]
        public IActionResult GetOccupations()
        {
            var result = new Result<List<OccupationResult>>();
            var response = _accountOpeningService.GetOccupations();
            result.Content = response;
            result.ResponseCode = "000";
            return Ok(result);
        }

        [HttpGet("GetEmploymentStatus/")]
        public IActionResult GetEmploymentStatus()
        {
            var result = new Result<List<EmploymentResult>>();
            var response = _accountOpeningService.GetEmploymentStatus();
            result.Content = response;
            result.ResponseCode = "000";
            return Ok(result);
        }
    }
}