using StanbicIBTC.AccountOpening.Data;
using StanbicIBTC.AccountOpening.Service;

namespace StanbicIBTC.AccountOpening.API.Controllers.v1;

public class BulkAccountOpeningController : BaseController
{
    private readonly IBulkAccountOpeningService _service;

    public BulkAccountOpeningController(IBulkAccountOpeningService service)
    {
        _service = service;
    }

    [HttpPost("UploadAccountFile/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public IActionResult UploadAccountFile([FromForm] BulkAccountRequestDto accountrequest)
    {
        var result = new Result<string>();
        var response = _service.UploadFile(accountrequest);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpPost("ApproveOrRejectFile/")]
    [ProducesResponseType(200, Type = typeof(Result<string>))]
    public async Task<IActionResult> ApproveFile([FromBody] BulkAccountDto account)
    {
        var result = new Result<string>();
        var response = await _service.ApproveOrRejectFile(account);
        result.Content = response.responseDescription;
        result.ResponseCode = response.responseCode;
        return Ok(result);
    }

    [HttpGet("GetPendingBulkAccountRequests/")]
    [ProducesResponseType(200, Type = typeof(Result<List<BulkAccountDto>>))]
    public async Task<IActionResult> GetPendingBulkAccountRequestsByBranchId(string branchId)
    {
        var requests = await _service.GetBulkAccountRequestsByBranchId(branchId);
        return Ok(requests);
    }

    [HttpGet("GetSuccessfullyOpenedAccounts/")]
    [ProducesResponseType(200, Type = typeof(Result<List<BulkRecentActivities>>))]
    public async Task<IActionResult> GetSuccessfullyOpenedAccountsByBranchId(string branchId)
    {
        var accounts = await _service.GetSuccessfullyOpenedAccountByBranchId(branchId);
        return Ok(accounts);
    }

    [HttpGet("UploadHistory/")]
    [ProducesResponseType(200, Type = typeof(Result<PaginatedList<BulkAccountDto>>))]
    public async Task<IActionResult> UploadHistory([FromQuery] UploadHistoryDto history)
    {
        var uploads = await _service.UploadHistory(history);
        return Ok(uploads);
    }

    [HttpPost("OpenBulkAccounts/")]
    public async Task<IActionResult> OpenBulkAccounts(BulkAccountRequest request)
    {
        var requests = await _service.OpenBulkAccounts(request);
        return Ok(requests);
    }

    [HttpGet("GetApprovedRequests/")]
    public async Task<IActionResult> GetApprovedRequests(string branchId)
    {
        var requests = await _service.GetApprovedBulkAccountRequestsByBranchId(branchId);
        return Ok(requests);
    }

    [HttpGet("DownloadFile"),DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var file = await _service.DownloadFile(fileName);
        if (file is null)
        {
            return NotFound("The File does not exist");
        }
        return File(file.Memory,file.ContentType,file.FilePath);
    }
}