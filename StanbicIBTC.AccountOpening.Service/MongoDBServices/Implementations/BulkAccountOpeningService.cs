using OfficeOpenXml;

namespace StanbicIBTC.AccountOpening.Service;

public interface IBulkAccountOpeningService
{
    Task<ApiResult> ApproveOrRejectFile(BulkAccountDto request);
    List<BulkAccount> ReadFromExcel(string filePath);
    ApiResult UploadFile(BulkAccountRequestDto request);
}

public class BulkAccountOpeningService : IBulkAccountOpeningService
{
    private readonly ILogger<BulkAccountOpeningService> _logger;
    private readonly IBulkAccountRequestRepository _requestRepo;

    public BulkAccountOpeningService(ILogger<BulkAccountOpeningService> logger, IBulkAccountRequestRepository requestRepo)
    {
        _logger = logger;
        _requestRepo = requestRepo;
    }

    public ApiResult UploadFile(BulkAccountRequestDto request)
    {
        try
        {
            var batchId = DateTime.Now.ToString("yyyy MM dd HH mm ss").Replace(" ", string.Empty);

            var dir = $"{Directory.GetCurrentDirectory()}/Files";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (request.File == null)
            {
                _logger.LogInformation("There was no file sent");

            }

            FileInfo fileInfo = new FileInfo(request.File.FileName);


            if (fileInfo.Extension != ".xlsx")
            {
                _logger.LogInformation("The File received is not an excel file");

            }

            var uniqueFileName = batchId + "_" + request.File.FileName;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            request.File.CopyTo(new FileStream(filePath, FileMode.Create));

            var bulkRequest = new BulkAccountRequest
            {
                BranchId = request.BranchId,
                CreatedBy = request.CreatedBy,
                File = filePath
            };

            var requestId = _requestRepo.CreateBulkAccountRequest(bulkRequest);

            return new ApiResult { responseCode = "000", responseDescription = "File has been uploaded Successfully" };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "There was an error with your upload, Please try again later" };

        }
    }

    public async Task<ApiResult> ApproveOrRejectFile(BulkAccountDto request)
    {
        try
        {
            var requestInDb = await _requestRepo.GetBulkAccountRequest(request.BulkAccountRequestId);

            if (requestInDb is null)
            {
                return new ApiResult { responseCode = "999", responseDescription = "Invalid Request Sent" };
            }
            requestInDb.ApprovalStatus = request.ApprovalStatus;
            requestInDb.ApprovedBy = request.ApprovedBy;
            requestInDb.DateModified = DateTime.Now;
            var isSaved = await _requestRepo.UpdateBulkAccountRequest(request.BulkAccountRequestId, requestInDb);
            if (!isSaved)
            {
                return new ApiResult { responseCode = "999", responseDescription = "Request was unsuccessful, please try again later" };
            }
            return new ApiResult { responseCode = "000", responseDescription = "Request treated Successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "There was a problem processing your request, try again later" };
        }
    }
    public List<BulkAccount> ReadFromExcel(string filePath)
    {
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var sheet = package.Workbook.Worksheets[0]; // the 1st sheet in the excel file
            var result = GetListFromExcelSheet(sheet);
            return result;

        }
    }
    private List<BulkAccount> GetListFromExcelSheet(ExcelWorksheet sheet)
    {

        List<BulkAccount> list = new List<BulkAccount>();
        var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToList().Select(x => new
        {
            Index = x,
            ColumnName = sheet.Cells[1, x].Value.ToString().Trim()
        });

        for (int row = 2; row <= sheet.Dimension.Rows; row++)
        {
            BulkAccount obj = (BulkAccount)Activator.CreateInstance(typeof(BulkAccount));
            foreach (var prop in typeof(BulkAccount).GetProperties())
            {
                int col = columnInfo.SingleOrDefault(x => x.ColumnName == prop.Name).Index;
                var val = sheet.Cells[row, col].Value;
                var propType = prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(val, propType));
            }
            list.Add(obj);
        }
        return list;
    }
}