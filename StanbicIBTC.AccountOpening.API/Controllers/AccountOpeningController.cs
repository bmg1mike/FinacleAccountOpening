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
            result.Content = await _accountOpeningService.ValidateTierOneAccountOpeningRequest(request);
            return Ok(result);
        }
    }
}