namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public partial class OutboundLogsController : BaseController
{
    private readonly IOutboundLogService outboundLogService;

    public OutboundLogsController(IOutboundLogService outboundLogService)
    {
        this.outboundLogService = outboundLogService;
    }

    [HttpPost]
    [Route("GetOutboundLogs/")]
    public async Task<ActionResult> GetOutboundLogs()
    {
        var result = new Result<List<OutboundLog>>();
        result.Content = await outboundLogService.GetOutboundLogs();
        return Ok(result);
    }

    [HttpPost]
    [Route("GetOutboundLog/")]
    public async Task<ActionResult> GetOutboundLog(string id)
    {
        var result = new Result<OutboundLog>();

        var outboundLog = await outboundLogService.GetOutboundLog(id);

        if (outboundLog == null)
        {
            result.Error =  PopulateError(400,$"OutboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);
        }
        result.Content = outboundLog;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateOutboundLog/")]
    public async Task<ActionResult> CreateOutboundLog([FromBody] OutboundLog outboundLog)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid OutboundLog model","Invalid Model State");
            return BadRequest(result);
        }*/

        string outboundLogId = await outboundLogService.CreateOutboundLog(outboundLog);

        if (outboundLogId == "")
        {
            result.Error =  PopulateError(500,$"OutboundLog not created","Internal Server Error");
            return BadRequest(result);
        }

        result.Content = $"OutboundLog with Id {outboundLogId} Created Successfully !";

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateOutboundLog/")]
    public async Task<ActionResult>UpdateOutboundLog(string id, [FromBody] OutboundLog outboundLog)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid OutboundLog model","Invalid Model State");
            return BadRequest(result);
        } */

        var existingOutboundLog = await outboundLogService.GetOutboundLog(id);

        if (existingOutboundLog == null)
        {
            result.Error =  PopulateError(400,$"OutboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);
        }

        bool updateStatus = await outboundLogService.UpdateOutboundLog(id, outboundLog);

        if (!updateStatus)
        {
         result.Error =  PopulateError(500,$"OutboundLog with Id = {id} not Updated","Not Updated");
         return BadRequest(result);
        }
        result.Content = $"OutboundLog with Id = {id} Updated Successfully";

        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveOutboundLog")]
    public async Task<ActionResult>RemoveOutboundLog(string id)
    {
        var result = new Result<string>();

       /* if (outboundLog == null)
        {
            result.Error =  PopulateError(400,$"OutboundLog with Id = {id} not found","Not Found");
            return BadRequest(result);

        }*/

        bool deleted =  await outboundLogService.RemoveOutboundLog(id);
        if (!deleted)
        {
         result.Error =  PopulateError(500,$"OutboundLog with Id = {id} not deleted","Not Deleted");
         return BadRequest(result);
        }

        result.Content  = $"OutboundLog with Id = {id} deleted";
        return Ok(result);
    }
}
