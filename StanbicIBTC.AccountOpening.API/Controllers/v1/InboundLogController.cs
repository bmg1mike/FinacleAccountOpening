namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public partial class InboundLogsController : BaseController
{
    private readonly IInboundLogService inboundLogService;

    public InboundLogsController(IInboundLogService inboundLogService)
    {
        this.inboundLogService = inboundLogService;
    }

    [HttpPost]
    [Route("GetInboundLogs/")]
    public async Task<ActionResult> GetInboundLogs()
    {
        var result = new Result<List<InboundLog>>();
        result.Content = await inboundLogService.GetInboundLogs();
        return Ok(result);
    }

    [HttpPost]
    [Route("GetInboundLog/")]
    public async Task<ActionResult> GetInboundLog(string id)
    {
        var result = new Result<InboundLog>();

        var inboundLog = await inboundLogService.GetInboundLog(id);

        if (inboundLog == null)
        {
            result.Error =  PopulateError(400,$"InboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);
        }
        result.Content = inboundLog;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateInboundLog/")]
    public async Task<ActionResult> CreateInboundLog([FromBody] InboundLog inboundLog)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid InboundLog model","Invalid Model State");
            return BadRequest(result);
        }*/

        string inboundLogId = await inboundLogService.CreateInboundLog(inboundLog);

        if (inboundLogId == "")
        {
            result.Error =  PopulateError(500,$"InboundLog not created","Internal Server Error");
            return BadRequest(result);
        }

        result.Content = $"InboundLog with Id {inboundLogId} Created Successfully !";

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateInboundLog/")]
    public async Task<ActionResult>UpdateInboundLog(string id, [FromBody] InboundLog inboundLog)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid InboundLog model","Invalid Model State");
            return BadRequest(result);
        } */

        var existingInboundLog = await inboundLogService.GetInboundLog(id);

        if (existingInboundLog == null)
        {
            result.Error =  PopulateError(400,$"InboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);
        }

        bool updateStatus = await inboundLogService.UpdateInboundLog(id, inboundLog);

        if (!updateStatus)
        {
         result.Error =  PopulateError(500,$"InboundLog with Id = {id} not Updated","Not Updated");
         return BadRequest(result);
        }
        result.Content = $"InboundLog with Id = {id} Updated Successfully";

        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveInboundLog")]
    public async Task<ActionResult>RemoveInboundLog(string id)
    {
        var result = new Result<string>();

       /* if (inboundLog == null)
        {
            result.Error =  PopulateError(400,$"InboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);

        }*/

        bool deleted =  await inboundLogService.RemoveInboundLog(id);
        if (!deleted)
        {
         result.Error =  PopulateError(500,$"InboundLog with Id = {id} not deleted","Not Deleted");
         return BadRequest(result);
        }

        result.Content  = $"InboundLog with Id = {id} deleted";
        return Ok(result);
    }
}
