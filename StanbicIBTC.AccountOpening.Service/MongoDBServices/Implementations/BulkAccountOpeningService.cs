using Microsoft.AspNetCore.StaticFiles;

namespace StanbicIBTC.AccountOpening.Service;

public class BulkAccountOpeningService : IBulkAccountOpeningService
{
    private readonly ILogger<BulkAccountOpeningService> _logger;
    private readonly IBulkAccountRequestRepository _requestRepo;
    private readonly IAccountOpeningService _accountOpeningService;
    private readonly IFinacleRepository _finacleRepository;
    private readonly ICIFRequestRepository _cifRepository;
    private readonly IInboundLogRepository _inboundLogRepository;
    private readonly IConfiguration _config;
    private readonly IAccountOpeningAttemptRepository _accountOpeningAttemptRepository;

    public BulkAccountOpeningService(ILogger<BulkAccountOpeningService> logger, IBulkAccountRequestRepository requestRepo, IAccountOpeningService accountOpeningService, IFinacleRepository finacleRepository, ICIFRequestRepository cifRepository, IInboundLogRepository inboundLogRepository, IConfiguration config, IAccountOpeningAttemptRepository accountOpeningAttemptRepository)
    {
        _logger = logger;
        _requestRepo = requestRepo;
        _accountOpeningService = accountOpeningService;
        _finacleRepository = finacleRepository;
        _cifRepository = cifRepository;
        _inboundLogRepository = inboundLogRepository;
        _config = config;
        _accountOpeningAttemptRepository = accountOpeningAttemptRepository;
    }

    public ApiResult UploadFile(BulkAccountRequestDto request)
    {
        try
        {
            var batchId = DateTime.Now.ToString("yyyy MM dd HH mm ss").Replace(" ", string.Empty);

            var dir = $"{Directory.GetCurrentDirectory()}/others/Bulk_Files";
            
            //var dir = _config["File_Path"];
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (request.File == null)
            {
                _logger.LogInformation("There was no file sent");
                return new ApiResult { responseCode = "999", responseDescription = "There was no file sent" };

            }

            FileInfo fileInfo = new FileInfo(request.File.FileName);


            if (fileInfo.Extension != ".xlsx")
            {
                _logger.LogInformation("The File received is not an excel file");
                return new ApiResult { responseCode = "999", responseDescription = "The File received is not an excel file" };

            }

            var uniqueFileName = $"{batchId}_{request.File.FileName}";

            //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            var uploadsFolder = dir;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            request.File.CopyTo(new FileStream(filePath, FileMode.Create));

            var bulkRequest = new BulkAccountRequest
            {
                BranchId = request.BranchId,
                CreatedBy = request.CreatedBy,
                File = uniqueFileName
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

    public async Task<DownloadFileResponse> DownloadFile(string fileName)   /// Todo
    {
        var dir = $"{Directory.GetCurrentDirectory()}/others/Bulk_Files";
        var filePath = $"{dir}/{fileName.Trim()}";//Path.Combine(Directory.GetCurrentDirectory(), $"/Files/{ fileName.Trim()}");
        if (!File.Exists(filePath))
        {
            return null;
        }
        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        GetContentType(filePath);

        var response = new DownloadFileResponse
        {
            Memory = memory,
            ContentType = GetContentType(filePath),
            FilePath = filePath
        };

        return response;
        //var file = $"{Directory.GetCurrentDirectory()}/Files/{fileName.Trim()}";

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

            if (request.ApprovedBy == requestInDb.CreatedBy)
            {
                return new ApiResult { responseCode = "999", responseDescription = "You Can't approve yourself" };
            }

            if (_finacleRepository.ValidateRelationshipManager(request.ApprovedBy) == null)
            {
                return new ApiResult { responseCode = "999", responseDescription = "You are not a registered branch manager" };
            }

            if (requestInDb.ApprovalStatus != ApprovalStatus.Pending)
            {
                return new ApiResult { responseCode = "999", responseDescription = "This request is no longer pending" };
            }
            requestInDb.ApprovalStatus = request.ApprovalStatus;
            requestInDb.ApprovedBy = request.ApprovedBy;
            requestInDb.DateModified = DateTime.Now;
            requestInDb.Comment = request.Comment;
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

    public async Task<Result<List<BulkAccountDto>>> GetBulkAccountRequestsByBranchId(string branchId)
    {
        var requests = await _requestRepo.GetPendingBulkAccountRequests(branchId);

        var requestsdto = new List<BulkAccountDto>();
        foreach (var item in requests)
        {
            requestsdto.Add(new BulkAccountDto
            {
                BranchId = item.BranchId,
                ApprovalStatus = item.ApprovalStatus,
                ApprovedBy = item.ApprovedBy,
                BulkAccountRequestId = item.BulkAccountRequestId,
                CreatedBy = item.CreatedBy,
                File = item.File,
                DateCreated = item.DateCreated
            });
        }

        return new Result<List<BulkAccountDto>> { Content = requestsdto, ResponseCode = "000" };
    }

    public async Task<Result<List<BulkAccountDto>>> GetApprovedBulkAccountRequestsByBranchId(string branchId)
    {
        var requests = await _requestRepo.GetApprovedBulkAccountRequests(branchId);

        var requestsdto = new List<BulkAccountDto>();
        foreach (var item in requests)
        {
            requestsdto.Add(new BulkAccountDto
            {
                BranchId = item.BranchId,
                ApprovalStatus = item.ApprovalStatus,
                ApprovedBy = item.ApprovedBy,
                BulkAccountRequestId = item.BulkAccountRequestId,
                CreatedBy = item.CreatedBy,
                File = item.File,
                DateCreated = item.DateCreated
            });
        }

        return new Result<List<BulkAccountDto>> { Content = requestsdto, ResponseCode = "000" };
    }
    public List<BulkAccount> ReadFromExcel(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var sheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "AccountOpening");
            if (sheet == null)
            {
                return null;
            }
            var result = GetListFromExcelSheet(sheet);
            return result;

        }
    }
    private List<BulkAccount> GetListFromExcelSheet(ExcelWorksheet sheet)
    {

        List<BulkAccount> list = new List<BulkAccount>();
        var columnInfo = Enumerable.Range(1, 6).ToList().Select(x => new       // Note :   6 is sheet.Dimension.Columns since we are expecting 6 columns
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

    public async Task<string> OpenBulkAccounts(BulkAccountRequest request)
    {
        try
        {
            var path = $"{Directory.GetCurrentDirectory()}/others/Bulk_Files{request.File}";

            var accounts = ReadFromExcel(path);
            if (accounts is null)
            {
                request.IsTreated = true;
                await _requestRepo.UpdateBulkAccountRequest(request.BulkAccountRequestId, request);
                return "Could not read Excel file successfully";
            }
            foreach (var item in accounts)
            {
                var savedData = await SaveBulkAccountRequest(item, request.BulkAccountRequestId);
            }

            request.IsTreated = true;
            await _requestRepo.UpdateBulkAccountRequest(request.BulkAccountRequestId, request);
            return "Excel data saved for processing successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return "Please try again later";
        }
    }

    private async Task<string> SaveBulkAccountRequest(BulkAccount request, string bulkAccountId)
    {
        try
        {
            var accountOpeningAttempt = new AccountOpeningAttempt
            {
                Bvn = request.Bvn,
                Response = string.Empty,
                PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber(),
                AccountTypeRequested = "Bulk",
                IsSuccessful = false
            };




            if (request.Bvn is null || request.DateOfBirth is null || request.PhoneNumber is null)
            {
                accountOpeningAttempt.Response = "Invalid request. Bvn, DateOfBirth and Phone Number are required to open a tier 1 account";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return null;
            }


            var bvnDetailsResponse = await _accountOpeningService.GetBVNDetails(request.Bvn);


            var outboundBvn = new OutboundLog
            {
                APICalled = "BVN Api",
                APIMethod = "GetBvnDetails",
                LogDate = DateTime.Now,
                RequestDateTime = DateTime.Now,
                ResponseDateTIme = DateTime.Now,
                SystemCalledName = "Account Opening Microservice",
                RequestDetails = String.Empty
            };

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                accountOpeningAttempt.Response = "Invalid Bvn";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "Invalid Bvn";
            }

            // if (string.IsNullOrEmpty(bvnDetailsResponse.data.NIN))
            // {
            //     _logger.LogInformation("The BVN is not linked to any NIN");
            //     accountOpeningAttempt.Response = "The BVN is not linked to any NIN";
            //     await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
            //     return "Invalid Bvn";
            // }

            var bvnDetails = bvnDetailsResponse.data;


            // var ninDetailsResponse = await _accountOpeningService.GetNinDetails(bvnDetails.NIN.Trim(), request.DateOfBirth.ToString()); // change back to BVN NIN i.e bvnDetails.NIN

            // if (ninDetailsResponse.data is null)
            // {
            //     _logger.LogInformation($"{ninDetailsResponse.responseDescription}");
            //     accountOpeningAttempt.Response = "InValid NIN";
            //     await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
            //     return "Invalid NIN";
            // }

            var outboundNin = new OutboundLog
            {
                APICalled = "NIN Api",
                APIMethod = "GetNinDetails",
                LogDate = DateTime.Now,
                RequestDateTime = DateTime.Now,
                ResponseDateTIme = DateTime.Now,
                SystemCalledName = "Account Opening Microservice",
                RequestDetails = String.Empty
            };

            //var ninDetails = ninDetailsResponse.data;


            if (bvnDetails.PhoneNumber.AsNigerianPhoneNumber() != request.PhoneNumber.AsNigerianPhoneNumber())
            {
                _logger.LogInformation($"The Phone number provided is not the same with the Bvn phone number. BVN : {bvnDetails.BVN}");
                accountOpeningAttempt.Response = "The Phone number provided is not the same with the Bvn phone number";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "Details given does not match with your BVN details";
            }

            var bvnDob = DateTime.Parse(bvnDetails.DateOfBirth);
            if (bvnDob.ToString("yyyy-MM-dd") != request.DateOfBirth)
            {
                _logger.LogInformation($"BVN Date Of Birth does not match the supplied Date Of Birth");
                accountOpeningAttempt.Response = "BVN Date Of Birth does not match the supplied Date Of Birth";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "Details given does not match with your BVN details";
            }

            if (string.IsNullOrEmpty(request.BranchManagerSapId) || string.IsNullOrEmpty(request.SolId))
            {
                accountOpeningAttempt.Response = "one or both BranchManager SapId and SolId  are empty";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return null;
            }

            var checkRelationshipManager = _finacleRepository.ValidateRelationshipManager(request.BranchManagerSapId.Trim());

            if (checkRelationshipManager == null)
            {
                _logger.LogInformation($"The Branch Manager is not a varified Branch Manager");
                accountOpeningAttempt.Response = "The Branch Manager is not a varified Branch Manager";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "The Branch Manager is not a varified Branch Manager";
            }

            if (checkRelationshipManager.Sol_Id != request.SolId.Trim())
            {
                _logger.LogInformation($"The Branch Manager does not belong to the branch given");
                accountOpeningAttempt.Response = "The Branch Manager does not belong to the branch given";
                await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "The Branch Manager does not belong to the branch given";
            }


            // var nextOfKinDetails = new CIFNextOfKinDetail
            // {
            //     FirstName = ninDetails.FullData.nok_firstname,
            //     LastName = ninDetails.FullData.nok_lastname,
            //     Address1 = ninDetails.FullData.nok_address1,
            //     Address2 = ninDetails.FullData.nok_address2,
            //     State = ninDetails.FullData.nok_state,
            //     Town = ninDetails.FullData.nok_town
            // };

            var secretQuestion = string.Empty;
            var secretAnswer = string.Empty;
            var password = string.Empty;
            var confirmPassword = string.Empty;

            var inbound = new InboundLog
            {
                LogDate = DateTime.Now,
                APICalled = "OpenTierOneAccount",
                APIMethod = "ValidateTierOneAccountOpeningRequest",
                OutboundLogs = new List<OutboundLog> { outboundBvn, outboundNin },
                RequestDateTime = DateTime.Now,
                RequestSystem = Environment.MachineName.ToString(),
                ImpactUniqueIdentifier = Guid.NewGuid().ToString(),
                AlternateUniqueIdentifier = Guid.NewGuid().ToString(),

            };
            await _inboundLogRepository.CreateInboundLog(inbound);

            switch (bvnDetails.Title.ToUpper())
            {
                case "MR":
                    bvnDetails.Title = "041";
                    break;
                case "MRS":
                    bvnDetails.Title = "042";
                    break;
                case "MISS":
                    bvnDetails.Title = "043";
                    break;
                case "MS":
                    bvnDetails.Title = "040";
                    break;
                default:
                    bvnDetails.Title = "175";
                    break;
            }

            var cifRequest = new CIFRequest
            {
                AccountTypeRequested = AccountTypeRequested.Bulk_Tier_One.ToString(),
                BvnEnrollmentBranch = bvnDetails.EnrollmentBranch,
                BvnErollmentBank = bvnDetails.EnrollmentBank,
                CustomerAddress = bvnDetails.ResidentialAddress,
                AccountOpeningStatus = AccountOpeningStatus.Pending.ToString(),
                EmploymentStatus = "009",
                OccupationCode = "999",
                FirstName = bvnDetails.FirstName,
                LastName = bvnDetails.LastName,
                CustomerBVN = bvnDetails.BVN,
                DateOfBirthInY_M_D_Format = bvnDetails.DateOfBirth,
                Email = bvnDetails.Email,
                StateOfResidence = bvnDetails.StateOfResidence,
                MaritalStatus = Util.MaritalStatusCode(bvnDetails.MaritalStatus),
                LgaOfResidence = bvnDetails.LgaOfResidence,
                NIN = bvnDetails.NIN,
                PhoneNumber = bvnDetails.PhoneNumber,
                Gender = bvnDetails.Gender,
                Platform = Platform.Bulk.ToString(),
                MiddleName = bvnDetails.MiddleName,
                SecretQuestion = secretQuestion,
                SecretAnswer = secretAnswer,
                Password = password,
                ConfirmPassword = confirmPassword,
                //NextOfKinDetail = nextOfKinDetails,
                Title = bvnDetails.Title,
                SolId = request.SolId,
                Category = request.Category,
                BranchManagerSapId = request.BranchManagerSapId,
                BulkAccountId = bulkAccountId
            };

            var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            if (saveCifRequest == null)
            {
                _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
                return "There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later";
            }



            accountOpeningAttempt.Response = "Request successfully stored in the database for processing";
            accountOpeningAttempt.IsSuccessful = true;
            var accountOpeningAttemptId = await _accountOpeningAttemptRepository.CreateAccountOpeningAttempt(accountOpeningAttempt);
            //var accountNumber = await OpenAccount(cifRequest);


            return $"Request successfully stored in the database for processing";

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ex.Message;
        }
    }

    public async Task<Result<PaginatedList<BulkAccountDto>>> UploadHistory(UploadHistoryDto history)
    {
        try
        {
            var requests = await _requestRepo.GetAllAccountRequests(history);
            return new Result<PaginatedList<BulkAccountDto>> { Content = requests, ResponseCode = "000" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Result<PaginatedList<BulkAccountDto>> { Content = null, ResponseCode = "999" };
        }
    }


    public async Task<Result<List<BulkRecentActivities>>> GetSuccessfullyOpenedAccountByBranchId(string branchId,string bulkAccountId)
    {
        try
        {
            var accounts = await _cifRepository.GetSuccessfullyOpenedAccountsByBranchId(branchId,bulkAccountId);

            var bulkAccountActivities = new List<BulkRecentActivities>();
            foreach (var item in accounts)
            {
                bulkAccountActivities.Add(new BulkRecentActivities
                {

                    AccountOpeningStatus = item.AccountOpeningStatus,
                    Bvn = item.CustomerBVN,
                    ManagerSapId = item.BranchManagerSapId,
                    PhoneNumber = item.PhoneNumber,
                    SolId = item.SolId
                }
                );
            }

            return new Result<List<BulkRecentActivities>> { Content = bulkAccountActivities, ResponseCode = "000" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Result<List<BulkRecentActivities>> { Content = null, ResponseCode = "999" };
        }
    }

    public async Task<List<BulkAccountRequest>> GetApprovedRequests()
    {
        try
        {
            var requests = await _requestRepo.GetApprovedRequests();
            return requests;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    private string GetContentType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();
        string contentType;

        if (!provider.TryGetContentType(path, out contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }

}