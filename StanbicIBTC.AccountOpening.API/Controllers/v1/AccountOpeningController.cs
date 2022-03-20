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