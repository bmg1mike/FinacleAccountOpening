namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public partial class AccountOpeningAttemptsController : BaseController
{
    private readonly IAccountOpeningAttemptService accountOpeningAttemptService;

    public AccountOpeningAttemptsController(IAccountOpeningAttemptService accountOpeningAttemptService)
    {
        this.accountOpeningAttemptService = accountOpeningAttemptService;
    }

    [HttpPost]
    [Route("GetAccountOpeningAttempts/")]
    public async Task<ActionResult> GetAccountOpeningAttempts()
    {
        var result = new Result<List<AccountOpeningAttempt>>();
        result.Content = await accountOpeningAttemptService.GetAccountOpeningAttempts();
        return Ok(result);
    }

    [HttpPost]
    [Route("GetAccountOpeningAttempt/")]
    public async Task<ActionResult> GetAccountOpeningAttempt(string id)
    {
        var result = new Result<AccountOpeningAttempt>();

        var accountOpeningAttempt = await accountOpeningAttemptService.GetAccountOpeningAttempt(id);

        if (accountOpeningAttempt == null)
        {
            result.Error =  PopulateError(400,$"AccountOpeningAttempt with Id = {id} not found","Not Found");
            return BadRequest(result);
        }
        result.Content = accountOpeningAttempt;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateAccountOpeningAttempt/")]
    public async Task<ActionResult> CreateAccountOpeningAttempt([FromBody] AccountOpeningAttempt accountOpeningAttempt)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid AccountOpeningAttempt model","Invalid Model State");
            return BadRequest(result);
        }*/

        string accountOpeningAttemptId = await accountOpeningAttemptService.CreateAccountOpeningAttempt(accountOpeningAttempt);

        if (accountOpeningAttemptId == "")
        {
            result.Error =  PopulateError(500,$"AccountOpeningAttempt not created","Internal Server Error");
            return BadRequest(result);
        }

        result.Content = $"AccountOpeningAttempt with Id {accountOpeningAttemptId} Created Successfully !";

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateAccountOpeningAttempt/")]
    public async Task<ActionResult>UpdateAccountOpeningAttempt(string id, [FromBody] AccountOpeningAttempt accountOpeningAttempt)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid AccountOpeningAttempt model","Invalid Model State");
            return BadRequest(result);
        } */

        var existingAccountOpeningAttempt = await accountOpeningAttemptService.GetAccountOpeningAttempt(id);

        if (existingAccountOpeningAttempt == null)
        {
            result.Error =  PopulateError(400,$"AccountOpeningAttempt with Id = {id} not found","Not Found");
            return BadRequest(result);
        }

        bool updateStatus = await accountOpeningAttemptService.UpdateAccountOpeningAttempt(id, accountOpeningAttempt);

        if (!updateStatus)
        {
         result.Error =  PopulateError(500,$"AccountOpeningAttempt with Id = {id} not Updated","Not Updated");
         return BadRequest(result);
        }
        result.Content = $"AccountOpeningAttempt with Id = {id} Updated Successfully";

        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveAccountOpeningAttempt")]
    public async Task<ActionResult>RemoveAccountOpeningAttempt(string id)
    {
        var result = new Result<string>();

       /* if (accountOpeningAttempt == null)
        {
            result.Error =  PopulateError(400,$"AccountOpeningAttempt with Id = {id} not found","Not Found");
            return BadRequest(result);

        }*/

        bool deleted =  await accountOpeningAttemptService.RemoveAccountOpeningAttempt(id);
        if (!deleted)
        {
         result.Error =  PopulateError(500,$"AccountOpeningAttempt with Id = {id} not deleted","Not Deleted");
         return BadRequest(result);
        }

        result.Content  = $"AccountOpeningAttempt with Id = {id} deleted";
        return Ok(result);
    }
}
