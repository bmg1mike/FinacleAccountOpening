namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public partial class CIFRequestsController : BaseController
{
    private readonly ICIFRequestService cIFRequestService;

    public CIFRequestsController(ICIFRequestService cIFRequestService)
    {
        this.cIFRequestService = cIFRequestService;
    }

    [HttpPost]
    [Route("GetCIFRequests/")]
    public async Task<ActionResult> GetCIFRequests()
    {
        var result = new Result<List<CIFRequest>>();
        result.Content = await cIFRequestService.GetCIFRequests();
        return Ok(result);
    }

    [HttpPost]
    [Route("GetCIFRequest/")]
    public async Task<ActionResult> GetCIFRequest(string id)
    {
        var result = new Result<CIFRequest>();

        var cIFRequest = await cIFRequestService.GetCIFRequest(id);

        if (cIFRequest == null)
        {
            result.Error =  PopulateError(400,$"CIFRequest with Id = {id} not found","Not Found");
            return BadRequest(result);
        }
        result.Content = cIFRequest;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateCIFRequest/")]
    public async Task<ActionResult> CreateCIFRequest([FromBody] CIFRequest cIFRequest)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid CIFRequest model","Invalid Model State");
            return BadRequest(result);
        }*/

        string cIFRequestId = await cIFRequestService.CreateCIFRequest(cIFRequest);

        if (cIFRequestId == "")
        {
            result.Error =  PopulateError(500,$"CIFRequest not created","Internal Server Error");
            return BadRequest(result);
        }

        result.Content = $"CIFRequest with Id {cIFRequestId} Created Successfully !";

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateCIFRequest/")]
    public async Task<ActionResult>UpdateCIFRequest(string id, [FromBody] CIFRequest cIFRequest)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid CIFRequest model","Invalid Model State");
            return BadRequest(result);
        } */

        var existingCIFRequest = await cIFRequestService.GetCIFRequest(id);

        if (existingCIFRequest == null)
        {
            result.Error =  PopulateError(400,$"CIFRequest with Id = {id} not found","Not Found");
            return BadRequest(result);
        }

        bool updateStatus = await cIFRequestService.UpdateCIFRequest(id, cIFRequest);

        if (!updateStatus)
        {
         result.Error =  PopulateError(500,$"CIFRequest with Id = {id} not Updated","Not Updated");
         return BadRequest(result);
        }
        result.Content = $"CIFRequest with Id = {id} Updated Successfully";

        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveCIFRequest")]
    public async Task<ActionResult>RemoveCIFRequest(string id)
    {
        var result = new Result<string>();

       /* if (cIFRequest == null)
        {
            result.Error =  PopulateError(400,$"CIFRequest with Id = {id} not found","Not Found");
            return BadRequest(result);

        }*/

        bool deleted =  await cIFRequestService.RemoveCIFRequest(id);
        if (!deleted)
        {
         result.Error =  PopulateError(500,$"CIFRequest with Id = {id} not deleted","Not Deleted");
         return BadRequest(result);
        }

        result.Content  = $"CIFRequest with Id = {id} deleted";
        return Ok(result);
    }
}
