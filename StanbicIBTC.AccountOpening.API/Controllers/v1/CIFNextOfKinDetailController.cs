namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public partial class CIFNextOfKinDetailsController : BaseController
{
    private readonly ICIFNextOfKinDetailService cIFNextOfKinDetailService;

    public CIFNextOfKinDetailsController(ICIFNextOfKinDetailService cIFNextOfKinDetailService)
    {
        this.cIFNextOfKinDetailService = cIFNextOfKinDetailService;
    }

    [HttpPost]
    [Route("GetCIFNextOfKinDetails/")]
    public async Task<ActionResult> GetCIFNextOfKinDetails()
    {
        var result = new Result<List<CIFNextOfKinDetail>>();
        result.Content = await cIFNextOfKinDetailService.GetCIFNextOfKinDetails();
        return Ok(result);
    }

    [HttpPost]
    [Route("GetCIFNextOfKinDetail/")]
    public async Task<ActionResult> GetCIFNextOfKinDetail(string id)
    {
        var result = new Result<CIFNextOfKinDetail>();

        var cIFNextOfKinDetail = await cIFNextOfKinDetailService.GetCIFNextOfKinDetail(id);

        if (cIFNextOfKinDetail == null)
        {
            result.Error =  PopulateError(400,$"CIFNextOfKinDetail with Id = {id} not found","Not Found");
            return BadRequest(result);
        }
        result.Content = cIFNextOfKinDetail;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateCIFNextOfKinDetail/")]
    public async Task<ActionResult> CreateCIFNextOfKinDetail([FromBody] CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid CIFNextOfKinDetail model","Invalid Model State");
            return BadRequest(result);
        }*/

        string cIFNextOfKinDetailId = await cIFNextOfKinDetailService.CreateCIFNextOfKinDetail(cIFNextOfKinDetail);

        if (cIFNextOfKinDetailId == "")
        {
            result.Error =  PopulateError(500,$"CIFNextOfKinDetail not created","Internal Server Error");
            return BadRequest(result);
        }

        result.Content = $"CIFNextOfKinDetail with Id {cIFNextOfKinDetailId} Created Successfully !";

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateCIFNextOfKinDetail/")]
    public async Task<ActionResult>UpdateCIFNextOfKinDetail(string id, [FromBody] CIFNextOfKinDetail cIFNextOfKinDetail)
    {
        var result = new Result<string>();

        /*if (!ModelState.IsValid)
        {
            result.Error =  PopulateError(500,$"invalid CIFNextOfKinDetail model","Invalid Model State");
            return BadRequest(result);
        } */

        var existingCIFNextOfKinDetail = await cIFNextOfKinDetailService.GetCIFNextOfKinDetail(id);

        if (existingCIFNextOfKinDetail == null)
        {
            result.Error =  PopulateError(400,$"CIFNextOfKinDetail with Id = {id} not found","Not Found");
            return BadRequest(result);
        }

        bool updateStatus = await cIFNextOfKinDetailService.UpdateCIFNextOfKinDetail(id, cIFNextOfKinDetail);

        if (!updateStatus)
        {
         result.Error =  PopulateError(500,$"CIFNextOfKinDetail with Id = {id} not Updated","Not Updated");
         return BadRequest(result);
        }
        result.Content = $"CIFNextOfKinDetail with Id = {id} Updated Successfully";

        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveCIFNextOfKinDetail")]
    public async Task<ActionResult>RemoveCIFNextOfKinDetail(string id)
    {
        var result = new Result<string>();

       /* if (cIFNextOfKinDetail == null)
        {
            result.Error =  PopulateError(400,$"CIFNextOfKinDetail with Id = {id} not found","Not Found");
            return BadRequest(result);

        }*/

        bool deleted =  await cIFNextOfKinDetailService.RemoveCIFNextOfKinDetail(id);
        if (!deleted)
        {
         result.Error =  PopulateError(500,$"CIFNextOfKinDetail with Id = {id} not deleted","Not Deleted");
         return BadRequest(result);
        }

        result.Content  = $"CIFNextOfKinDetail with Id = {id} deleted";
        return Ok(result);
    }
}
