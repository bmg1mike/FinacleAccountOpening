using System.Text;
using System.Text.RegularExpressions;

namespace StanbicIBTC.AccountOpening.Service;


public class AccountOpeningService : IAccountOpeningService
{
    private readonly ISoapRequestHelper _soapRequestHelper;
    private readonly ILogger<AccountOpeningService> _logger;
    private readonly IRestRequestHelper _restRequestHelper;
    private readonly IConfiguration _config;
    private readonly ICIFRequestRepository _cifRepository;
    private readonly IAccountOpeningAttemptRepository _accountOpeningAttempt;
    private readonly IFinacleRepository _finacleRepository;
    private readonly ISmsNotification _smsNotification;
    private readonly DataContext _modelContext;
    private readonly IOutboundLogRepository _outboundLogRepository;
    private readonly IInboundLogRepository _inboundLogRepository;

    public AccountOpeningService(ILogger<AccountOpeningService> logger, ISoapRequestHelper soapRequestHelper,
            ICIFRequestRepository cifRepository, IAccountOpeningAttemptRepository accountOpeningAttempt,
            IConfiguration config, IRestRequestHelper restRequestHelper, IFinacleRepository finacleRepository,
            ISmsNotification smsNotification, DataContext modelContext, IOutboundLogRepository outboundLogRepository, IInboundLogRepository inboundLogRepository)
    {
        _logger = logger;
        _soapRequestHelper = soapRequestHelper;
        _cifRepository = cifRepository;
        _accountOpeningAttempt = accountOpeningAttempt;
        _config = config;
        _restRequestHelper = restRequestHelper;
        _finacleRepository = finacleRepository;
        _smsNotification = smsNotification;
        _modelContext = modelContext;
        _outboundLogRepository = outboundLogRepository;
        _inboundLogRepository = inboundLogRepository;
    }

    public async Task<ApiResult> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request)
    {
        try
        {
            var bvnDetailsResponse = await GetBVNDetails(request.Bvn);

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
                return new ApiResult { responseCode = "999", responseDescription = "Invalid Bvn" };
            }

            var bvnDetails = bvnDetailsResponse.data;

            //if (bvnDetails.NIN != request.Nin)
            //{
            //    return new ApiResult { responseCode = "999", responseDescription = "NIN does not match BVN's NIN" };
            //}

            var ninDetailsResponse = await GetNinDetails(request.Nin, request.DateOfBirth.ToString());

            if (ninDetailsResponse.data is null)
            {
                _logger.LogInformation($"{ninDetailsResponse.responseDescription}");
                return new ApiResult { responseCode = "999", responseDescription = "Invalid NIN" };
            }

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

            var ninDetails = ninDetailsResponse.data;

            var accountOpeningAttempt = new AccountOpeningAttempt
            {
                FirstName = bvnDetails.FirstName,
                LastName = bvnDetails.LastName,
                Bvn = request.Bvn,
                Response = string.Empty,
                PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber(),
                AccountTypeRequested = "Tier 1"
            };

            if (bvnDetails.PhoneNumber.AsNigerianPhoneNumber() != request.PhoneNumber.AsNigerianPhoneNumber())
            {
                _logger.LogInformation($"The Phone number provided is not the same with the Bvn phone number. BVN : {bvnDetails.BVN}");
                accountOpeningAttempt.Response = "The Phone number provided is not the same with the Bvn phone number";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return new ApiResult { responseCode = "999", responseDescription = "Details given does not match with your BVN details" };
            }

            var bvnDob = DateTime.Parse(bvnDetails.DateOfBirth);
            if (bvnDob.ToString("dd-MM-yyyy") != request.DateOfBirth.ToString("dd-MM-yyyy"))
            {
                _logger.LogInformation($"BVN Date Of Birth does not match the supplied Date Of Birth");
                accountOpeningAttempt.Response = "BVN Date Of Birth does not match the supplied Date Of Birth";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return new ApiResult { responseCode = "999", responseDescription = "Details given does not match with your BVN details" };
            }
            var occupationCode = _finacleRepository.GetOccupations().FirstOrDefault(x => x.OccupationCode == request.OccupationCode);
            var employmentStatus = _finacleRepository.GetEmploymentStatus().FirstOrDefault(x => x.EmploymentStatusCode == request.EmploymentStatusCode);

            if (occupationCode is null || employmentStatus is null)
                return new ApiResult { responseCode = "999", responseDescription = "Invalid Occupation Code Or Employment status" };

            


            var nextOfKinDetails = new CIFNextOfKinDetail
            {
                FirstName = ninDetails.FullData.nok_firstname,
                LastName = ninDetails.FullData.nok_lastname,
                Address1 = ninDetails.FullData.nok_address1,
                Address2 = ninDetails.FullData.nok_address2,
                State = ninDetails.FullData.nok_state,
                Town = ninDetails.FullData.nok_town
            };

            var secretQuestion = string.Empty;
            var secretAnswer = string.Empty;
            var password = string.Empty;
            var confirmPassword = string.Empty;

            if (request.WillOnboard)
            {
                secretQuestion = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.SecretQuestion));
                secretAnswer = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.SecretAnswer));
                password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));
                confirmPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.ConfirmPassword));
            }

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

            var cifRequest = new CIFRequest
            {
                AccountTypeRequested = "Tier One",
                BvnEnrollmentBranch = bvnDetails.EnrollmentBranch,
                BvnErollmentBank = bvnDetails.EnrollmentBank,
                CustomerAddress = bvnDetails.ResidentialAddress,
                AccountOpeningStatus = AccountOpeningStatus.Pending.ToString(),
                EmploymentStatus = request.EmploymentStatusCode,
                OccupationCode = request.OccupationCode,
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
                Platform = request.Platform.ToString(),
                MiddleName = bvnDetails.MiddleName,
                WillOnBoard = request.WillOnboard,
                SecretQuestion = secretQuestion,
                SecretAnswer = secretAnswer,
                Password = password,
                ConfirmPassword = confirmPassword,
                NextOfKinDetail = nextOfKinDetails
            };

            var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            if (saveCifRequest == null)
            {
                _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later" };
            }



            //accountOpeningAttempt.Response = "Request successfully stored in the database for processing";
            //var accountOpeningAttemptId = await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
            //var accountNumber = await OpenAccount(cifRequest);


            return new ApiResult { responseCode = "000", responseDescription = $"Request successfully stored in the database for processing" };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = ex.Message };
        }
    }



    public async Task<ApiResult> UpgradeToTierThreeAccountOpeningRequest(TierOneUgrade request)
    {
        try
        {

            var accountDetails = _finacleRepository.GetAccountDetailsByAccountNumber(request.AccountNumber);
            
            if (accountDetails == null)
            {
                _logger.LogInformation("Account number does not exist");
                return new ApiResult { responseCode = "999", responseDescription = "You are not allowed to upgrade since your account is invalid" };
            }

            if (accountDetails.SchemeCode != "KYCL1")
            {
                _logger.LogInformation($"{request.AccountNumber} is already a tier 3 account");
                return new ApiResult { responseCode = "999", responseDescription = "Account is already a Tier 3 Account" };
            }

            var bvnDetailsResponse = await GetBVNDetails(request.Bvn);

            if (!Util.IsBase64String(request.PassportPhotograph)
                || !Util.IsBase64String(request.IdImage)
                || !Util.IsBase64String(request.Signature) || !Util.IsBase64String(request.UtilityBill))
            {
                return new ApiResult { responseCode = "999", responseDescription = "PassportPhotograph,IdImage and Signature must be Base 64 strings" };
            }

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                return new ApiResult { responseCode = "999", responseDescription = "Invalid Bvn" };
            }

            var cif = _finacleRepository.CheckCifForBvn(request.Bvn).Cif;

            var sanctionScreeningRequest = new SanctionScreeningRequest
            {
                CustomerBvn = bvnDetailsResponse.data.BVN,
                CustomerFirstName = bvnDetailsResponse.data.FirstName,
                CustomerFullHomeAddress = bvnDetailsResponse.data.ResidentialAddress,
                CustomerLastName = bvnDetailsResponse.data.LastName,
                CustomerMiddleName = bvnDetailsResponse.data.MiddleName
            };
            
            var sanctionScreening = _finacleRepository.LogForSanctionScreening(sanctionScreeningRequest);

            if (!sanctionScreening)
            {
                _logger.LogInformation($"There was a problem logging for Sanction Screening");
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem logging for Sanction Screening for BVN: {request.Bvn} Please try again later" };
            
            }

            var addressVerificationRequest = await _soapRequestHelper.LogAddressVerification(
                AccountOpeningPayloadHelper.AddressVerificationRequestPayload(bvnDetailsResponse.data.ResidentialAddress,cif));

                if (addressVerificationRequest.ResponseCode != "000")
                {
                    _logger.LogInformation($"There was a problem logging for Address Verification");
                    return new ApiResult { responseCode = "999", responseDescription = $"There was a problem logging for Address Verification for BVN: {request.Bvn} Please try again later" };
            
                }

                var addressverificationId =  _soapRequestHelper.GetXmlTagValue<string>(addressVerificationRequest.ResponseDescription,"LogAddressVerificationRequestResult");

               
                
                    var photographResponse = await SaveDocumentToDataStore(cif, request.PassportPhotograph,"Customer Photograph");
                    if (photographResponse.OutCome.Status != "SUCCESS")
                    {
                        _logger.LogInformation($"There was a problem saving Customer's photo to Data Store");
                        return new ApiResult { responseCode = "999", responseDescription = $"There was a problem upgrading your account, Please try again later" };
                    }
                
                
                    var idImageResponse = await SaveDocumentToDataStore(cif, request.IdImage,"Custormer's ID Card");
                    if (idImageResponse.OutCome.Status != "SUCCESS")
                    {
                        _logger.LogInformation($"There was a problem saving Customer's regulatory Id to Data Store");
                        return new ApiResult { responseCode = "999", responseDescription = $"There was a problem upgrading your account, Please try again later" };
                    }
                

                
                    var signatureResponse = await SaveDocumentToDataStore(cif, request.Signature,"Customer's Signature");
                    if (signatureResponse.OutCome.Status != "SUCCESS")
                    {
                        
                        _logger.LogInformation($"There was a problem saving Customer's signature to Data Store");
                        return new ApiResult { responseCode = "999", responseDescription = $"There was a problem upgrading your account, Please try again later" };
                    }
                
                
                    var utilityBillResponse = await SaveDocumentToDataStore(cif, request.UtilityBill,"Customer's Utility Bill");
                    if (utilityBillResponse.OutCome.Status != "SUCCESS")
                    {
                        _logger.LogInformation($"There was a problem saving Customer's Utility Bill to Data Store");
                        return new ApiResult { responseCode = "999", responseDescription = $"There was a problem upgrading your account, Please try again later" };
                    }
                
            

            var cifRequest = new CIFRequest
            {
                AccountNumber = request.AccountNumber,
                AccountTypeRequested = "Tier one Upgrade",
                BvnEnrollmentBranch = bvnDetailsResponse.data.EnrollmentBranch,
                BvnErollmentBank = bvnDetailsResponse.data.EnrollmentBank,
                Cif = cif,
                CustomerAddress = bvnDetailsResponse.data.ResidentialAddress,
                CustomerBVN = request.Bvn,
                Email = request.Email,
                DateOfBirthInY_M_D_Format = bvnDetailsResponse.data.DateOfBirth,
                EmployerAddress = request.EmployerAddress,
                EmployerName = request.EmployerName,
                IdentityType = request.IdentityType,
                IdExpiryDate = request.IdExpiryDate,
                IdIssueDate = request.IdIssueDate,
                IdNumber = request.IdNumber,
                FirstName = bvnDetailsResponse.data.FirstName,
                LastName = bvnDetailsResponse.data.LastName,
                MonthlyIncome = request.MonthlyIncome,
                PhoneNumber = bvnDetailsResponse.data.PhoneNumber.AsNigerianPhoneNumber(),
                SanctionScreeningAccountId = sanctionScreeningRequest.AccountOpeningRequestId,
                AddressverificationId = addressverificationId,
                AccountOpeningStatus = AccountOpeningStatus.Pending.ToString(),
                IsKycDocumentsUploaded = true,
                IsAddressVerificationLogged = true,
                IsSanctionScreeningLogged = true,
                Platform = request.Platform.ToString()

            };


            var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            if (saveCifRequest == null)
            {
                _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later" };
            }

            

            

            return new ApiResult { responseCode = "000", responseDescription = $"Request successfully stored in the database for processing" };
           

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "There was a problem uprading your account. Please try again later" };
        }
    }

    public async Task<ApiResult> OpenChessAccount(ChessAccountRequest request)
    {
        try
        {
            var cifDetails = _finacleRepository.CheckCifForBvn(request.Bvn);
            if (cifDetails is null)
            {
                return new ApiResult { responseCode = "999", responseDescription = "You must have an account with us before you can open an account for your child" };
            }

            var cifRequest = new CIFRequest
            {
                FirstName = request.ChildFirstName,
                LastName = request.ChildLastName,
                
            };

            if (!Util.IsBase64String(request.ChildBirthCertificate))
            {
                return new ApiResult { responseCode = "999", responseDescription = "Birth Certificate must be a Base 64 string" };
            }
            var birthCertificate = await SaveDocumentToDataStore(cifDetails.Cif, request.ChildBirthCertificate,"Birth Certificate");
            var photograph = await SaveDocumentToDataStore(cifDetails.Cif, request.Photograph,"Customer Photograph");

            var openAccount = await _soapRequestHelper.FinacleCall(
                AccountOpeningPayloadHelper.AccountOpeningPayload(cifDetails.Cif, cifRequest, "CHESS"));

            var accountNumber = _soapRequestHelper.GetXmlTagValue<string>(openAccount.ResponseDescription, "AcctId");
            if (string.IsNullOrEmpty(accountNumber))
            {
                return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later " };
            }

            return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Chess Account Number Is {accountNumber}." };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "There was a problem opening your account, Please try again later" };
        }
    }

    public async Task<ApiResult> OpenCurrentAccout(CurrentAccountRequest request)
    {
        try
        {
            var bvnDetailsResponse = await GetBVNDetails(request.Bvn);

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                return new ApiResult { responseCode = "999", responseDescription = bvnDetailsResponse.responseDescription };
            }

            var bvnDetails = bvnDetailsResponse.data;

            var nextOfKinDetails = new CIFNextOfKinDetail
            {
                FirstName = request.NextOfKin.FirstName,
                LastName = request.NextOfKin.LastName,
                MiddleName = request.NextOfKin.MiddleName,
                PhoneNumber = request.NextOfKin.PhoneNumber,
                Address1 = request.NextOfKin.Address,
                Address2 = request.NextOfKin.Address,
                State = request.NextOfKin.StateOfResidence,
                Town = request.NextOfKin.Town
            };

            var cifRequest = new CIFRequest
            {
                BvnEnrollmentBranch = bvnDetails.EnrollmentBranch,
                BvnErollmentBank = bvnDetails.EnrollmentBank,
                CustomerAddress = bvnDetails.ResidentialAddress,
                AccountOpeningStatus = AccountOpeningStatus.Pending.ToString(),
                EmploymentStatus = request.EmploymentStatusCode,
                OccupationCode = request.OccupationCode,
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
                Platform = request.Platform.ToString(),
                MiddleName = bvnDetails.MiddleName,
                NextOfKinDetail = nextOfKinDetails
            };

            if (request.AccountNumber is not null)
            {
                var cif = _finacleRepository.GetAccountDetailsByAccountNumber(request.AccountNumber).Cif;
                if (cif is null)
                {
                    return new ApiResult { responseCode = "999", responseDescription = "Invalid Account Number" };
                }



                var openAccount = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.AccountOpeningPayload(cif, cifRequest, "OD002"));
                var accountNumber = _soapRequestHelper.GetXmlTagValue<string>(openAccount.ResponseDescription, "AcctId");

                if (string.IsNullOrEmpty(accountNumber))
                {
                    return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later " };
                }

                return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Current Account Number Is {accountNumber}." };
            }

            var accountNumberForFirstTimeCustomer = await OpenAccount(cifRequest);
            if (accountNumberForFirstTimeCustomer is null)
            {
                return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later " };
            }

            return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Current Account Number Is {accountNumberForFirstTimeCustomer}." };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later " };
        }
    }

    public async Task OpenDomiciliaryAccount()
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResult> OpenTierThreeAccount(TierThreeAccountOpeningRequest request)
    {
        try
        {
            var bvnDetailsResponse = await GetBVNDetails(request.Bvn);

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                return new ApiResult { responseCode = "999", responseDescription = bvnDetailsResponse.responseDescription };
            }

            var bvnDetails = bvnDetailsResponse.data;

            var accountOpeningAttempt = new AccountOpeningAttempt
            {
                FirstName = bvnDetails.FirstName,
                LastName = bvnDetails.LastName,
                Bvn = request.Bvn,
                Response = string.Empty,
                PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber()
            };

            if (bvnDetails.PhoneNumber.AsNigerianPhoneNumber() != request.PhoneNumber.AsNigerianPhoneNumber())
            {
                _logger.LogInformation($"The Phone number provided is not the same with the Bvn phone number. BVN : {bvnDetails.BVN}");
                accountOpeningAttempt.Response = "The Phone number provided is not the same with the Bvn phone number";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return new ApiResult { responseCode = "999", responseDescription = "Details given does not match with your BVN details" };
            }

            var bvnDob = DateTime.Parse(bvnDetails.DateOfBirth);
            if (bvnDob.ToString("dd-MM-yyyy") != request.DateOfBirth.ToString("dd-MM-yyyy"))
            {
                _logger.LogInformation($"BVN Date Of Birth does not match the supplied Date Of Birth");
                accountOpeningAttempt.Response = "BVN Date Of Birth does not match the supplied Date Of Birth";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return new ApiResult { responseCode = "999", responseDescription = "Details given does not match with your BVN details" };
            }

            if (!Util.IsBase64String(request.PassportPhotograph)
                || !Util.IsBase64String(request.IdImage)
                || !Util.IsBase64String(request.Signature) || !Util.IsBase64String(request.UtilityBill))
            {
                return new ApiResult { responseCode = "999", responseDescription = "PassportPhotograph,IdImage and Signature must be Base 64 strings" };
            }

            var cif = _finacleRepository.CheckCifForBvn(request.Bvn).Cif;

            var sanctionScreeningRequest = new SanctionScreeningRequest
            {
                CustomerBvn = bvnDetailsResponse.data.BVN,
                CustomerFirstName = bvnDetailsResponse.data.FirstName,
                CustomerFullHomeAddress = bvnDetailsResponse.data.ResidentialAddress,
                CustomerLastName = bvnDetailsResponse.data.LastName,
                CustomerMiddleName = bvnDetailsResponse.data.MiddleName
            };

            var sanctionScreening = _finacleRepository.LogForSanctionScreening(sanctionScreeningRequest);

            if (!sanctionScreening)
            {
                _logger.LogInformation($"There was a problem logging for Sanction Screening");
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem logging for Sanction Screening for BVN: {request.Bvn} Please try again later" };
            
            }

            

            var cifRequest = new CIFRequest
            {
                AccountTypeRequested = "Tier Three",
                BvnEnrollmentBranch = bvnDetails.EnrollmentBranch,
                BvnErollmentBank = bvnDetails.EnrollmentBank,
                CustomerAddress = bvnDetails.ResidentialAddress,
                AccountOpeningStatus = AccountOpeningStatus.Pending.ToString(),
                EmploymentStatus = request.EmploymentStatusCode,
                OccupationCode = request.OccupationCode,
                FirstName = bvnDetails.FirstName,
                LastName = bvnDetails.LastName,
                CustomerBVN = bvnDetails.BVN,
                DateOfBirthInY_M_D_Format = bvnDetails.DateOfBirth,
                Email = bvnDetails.Email,
                StateOfResidence = request.StateOfResidence,
                MaritalStatus = Util.MaritalStatusCode(bvnDetails.MaritalStatus),
                LgaOfResidence = bvnDetails.LgaOfResidence,
                NIN = bvnDetails.NIN,
                PhoneNumber = bvnDetails.PhoneNumber,
                Gender = bvnDetails.Gender,
                Platform = request.Platform.ToString(),
                MiddleName = bvnDetails.MiddleName,
                NextOfKinDetail = new CIFNextOfKinDetail
                {
                    Address1 = request.NextOfKinDetails.Address,
                    FirstName = request.NextOfKinDetails.FirstName,
                    LastName = request.NextOfKinDetails.LastName,
                    DateOfBirthInY_M_DFormat = request.NextOfKinDetails.DateOfBirth.ToString("yyyy-MM-dd"),
                    MiddleName = request.NextOfKinDetails.MiddleName,
                    PhoneNumber = request.NextOfKinDetails.PhoneNumber,
                    State = request.NextOfKinDetails.StateOfResidence,
                    Town = request.NextOfKinDetails.Town,

                },
                SanctionScreeningAccountId = sanctionScreeningRequest.AccountOpeningRequestId,
                IdentityType = request.IdentityType,
                IdExpiryDate = request.IdExpiryDate,
                IdIssueDate = request.IdIssueDate,
                IdNumber = request.IdNumber

            };
            // Check if CIF exist

            var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            if (saveCifRequest == null)
            {
                _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later" };
            }

            return new ApiResult { responseCode = "000", responseDescription = "Your details has been saved for processing"};
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later" };
        }
    }

    public async Task<List<ApiResult>> BulkTierOneAccountOpening(List<TierOneAccountOpeningRequest> requests)
    {
        try
        {
            var apiResults = new List<ApiResult>();
            foreach (var item in requests)
            {
                var accountOpened = await ValidateTierOneAccountOpeningRequest(item);
                apiResults.Add(accountOpened);
            }
            return apiResults;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public List<EmploymentResult> GetEmploymentStatus()
    {
        var employments = _finacleRepository.GetEmploymentStatus();
        return employments;
    }

    public List<OccupationResult> GetOccupations()
    {
        var occupations = _finacleRepository.GetOccupations();
        return occupations;
    }

    public async Task<string> OpenAccount(CIFRequest request)
    {
        try
        {
            //var accountOpeningAttempt = await _accountOpeningAttempt.GetAccountOpeningAttempt(request.AccountOpeningAttemptId);

            //var cifRequest = await _cifRepository.GetCIFRequest(request.CIFRequestId);
            request.StateOfResidence = _finacleRepository.GetStateCode(request.StateOfResidence.ToUpper().Replace("STATE", "").Trim());
            if (string.IsNullOrEmpty(request.StateOfResidence))
            {
                request.IsAccountOpenedSuccessfully = false;
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                request.Response = "The state in the BVN is not available in the finacle Database";
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                return null;
            }

            request.LgaOfResidence = _finacleRepository.GetCityCode(request.LgaOfResidence.ToUpper().Trim());
            if (string.IsNullOrEmpty(request.LgaOfResidence))
            {
                request.IsAccountOpenedSuccessfully = false;
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                request.Response = "The City in the BVN is not available in the finacle Database";
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                return null;
            }

            //var existingCif = _finacleRepository.CheckCifForBvn(request.CustomerBVN);
            //FinacleAccountDetailResponse checkAccount = null;
            //if (existingCif.Cif is not null)
            //{
            //    checkAccount = _finacleRepository.GetAccountDetailsByCif(existingCif.Cif);
            //}
            //if (checkAccount is not null)
            //{
            //    return "This Customer already has an account";
            //}

            var cifResponse = await CreateCIF(request);

            if (cifResponse.cif == null)
            {
                request.IsAccountOpenedSuccessfully = false;
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                request.Response = "Couldn't create Cif from Finacle( Finacle not reachable )";
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                _logger.LogInformation($"There was a problem creating CIF for this BVN : {request.CustomerBVN} ");
                return null;
            }

            var redboxResult = await SaveTierOneCustomData(cifResponse.cif, request);
            if (!redboxResult)
            {
                request.IsAccountOpenedSuccessfully = false;
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                request.Response = "There was a problem saving the request to the redbox database";
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                return ($"There was a problem saving the CIF Request for BVN: {request.CustomerBVN} Please try again later");
            }

            if(request.AccountTypeRequested == "Tier Three")
            {
                var addressVerificationRequest = await _soapRequestHelper.LogAddressVerification(
                AccountOpeningPayloadHelper.AddressVerificationRequestPayload(request.CustomerAddress,cifResponse.cif));

                if (addressVerificationRequest.ResponseCode != "000")
                {
                    _logger.LogInformation($"There was a problem logging for Address Verification");
                    return null;
            
                }

                var addressverificationId =  _soapRequestHelper.GetXmlTagValue<string>(addressVerificationRequest.ResponseDescription,"LogAddressVerificationRequestResult");
                request.AddressverificationId = addressverificationId;
            }

            Thread.Sleep(120000);

            var openSavingsAccount = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.AccountOpeningPayload(cifResponse.cif, request));

            if (openSavingsAccount.ResponseCode != "000")
            {
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                request.IsAccountOpenedSuccessfully = false;
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                request.Response = "There was a problem saving the request to the Sql Server Db database";
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                return null;
            }


            var accountNumber = _soapRequestHelper.GetXmlTagValue<string>(openSavingsAccount.ResponseDescription, "AcctId");
            if (string.IsNullOrEmpty(accountNumber))
            {
                
                // await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return $"Could not open the account for CIF : {cifResponse.cif}";
            }
            //var cifRequest = await _cifRepository.GetCIFRequest(request.CIFRequestId);
            //cifRequest.AccountOpeningStatus = AccountOpeningStatus.Completed.ToString();
            //await _cifRepository.UpdateCIFRequest(cifRequest.CIFRequestId, cifRequest);

            var successMessage = string.Empty;
            if (request.WillOnBoard && (request.Platform == Platform.WEB.ToString() || request.Platform == Platform.MOBILE.ToString()))
            {
                if (string.IsNullOrEmpty(request.SecretQuestion) && string.IsNullOrEmpty(request.SecretAnswer) && string.IsNullOrEmpty(request.Password))
                {
                    return $"Your Account has been opened Successfully. Your Account Number Is {accountNumber}. Onboarding Failed for lack of adequate information ";
                }

                var isOnboarded = await MobileWebOnboarding(request, accountNumber);
                if (isOnboarded)
                {
                    successMessage = $"Your Account has been opened Successfully. Your Account Number Is {accountNumber}. You can now use the Mobile App and Internet banking";
                    await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, successMessage);

                    request.IsAccountOpenedSuccessfully = true;
                    request.AccountOpeningStatus = AccountOpeningStatus.Successful.ToString();
                    request.Response = "Tier One Account Opened Successfully and Onboarding was Successful";
                    request.Cif = cifResponse.cif;
                    request.AccountNumber = accountNumber;
                    await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                    return $"Account Number is {accountNumber}";
                }

                successMessage = $"Congratulations! Your account has been opened using your BVN details, Account Number ({accountNumber}). \n To get activated on our channels please visit https://ibanking.stanbicibtcbank.com/quickservices or our nearest branch.";
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, successMessage);
                request.IsAccountOpenedSuccessfully = true;
                request.AccountOpeningStatus = AccountOpeningStatus.Successful.ToString();
                request.Response = "Tier One Account Opened Successfully but Onboarding Failed";
                request.Cif = cifResponse.cif;
                request.AccountNumber = accountNumber;
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
                return $"Account Number is {accountNumber}";
            }
            request.IsAccountOpenedSuccessfully = true;
            request.AccountOpeningStatus = AccountOpeningStatus.Successful.ToString();
            request.Response = "Tier One Account Opened Successfully";
            request.Cif = cifResponse.cif;
            request.AccountNumber = accountNumber;
            await _cifRepository.UpdateCIFRequest(request.CIFRequestId, request);
            successMessage = $"Congratulations! Your account has been opened using your BVN details, Account Number ({accountNumber}). \n To get activated on our channels please visit https://ibanking.stanbicibtcbank.com/quickservices or our nearest branch.";
            await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, successMessage);
            return $"Account Number is {accountNumber}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    private VirtualAccountOpeningRequest CreateVirtualAccountWithoutBvn(CreateVirtualAccountDto request)
    {
        if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Gender)
            || string.IsNullOrEmpty(request.Address) || string.IsNullOrEmpty(request.PhoneNumber) ||
            string.IsNullOrEmpty(request.SecretWord))
        {
            return null;
        }

        if (request.Gender.ToUpper().Trim() == "FEMALE")
        {
            request.Gender = "F";
        }
        else if (request.Gender.ToUpper().Trim() == "MALE")
        {
            request.Gender="M";
        }
        else
        {
            return null;
        }
        
        //if (request.Gender.ToUpper() != "FEMALE" || request.Gender.ToUpper() != "MALE")
        //{
        //    return null;
        //}

        bool isValidPhoneNumber = Regex.IsMatch(request.PhoneNumber, @"^\d+$");
        if (!isValidPhoneNumber) return null;

        return new VirtualAccountOpeningRequest
        {
            BankVerificationNumber = "",
            Address = request.Address,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber(),
            DateOfBirth = request.DateOfBirth,
            SecretWord = request.SecretWord,
            ReferralCode = request.ReferralCode ?? "HPP00",
            RequestId = Util.GenerateRandomNumbers(15),
            SessionId = Guid.NewGuid().ToString(),
        };
    }

    private async Task<VirtualAccountOpeningRequest> CreateVirtualAccountWithBvn(CreateVirtualAccountDto request)
    {
        var bvnDetailsResponse = await GetBVNDetails(request.BankVerificationNumber);

        if (bvnDetailsResponse.data is null)
        {
            _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
            return null;
        }

        var bvnDetails = bvnDetailsResponse.data;

        bool isValidPhoneNumber = Regex.IsMatch(request.PhoneNumber, @"^\d+$");
        if (!isValidPhoneNumber) return null;

        if (request.PhoneNumber.AsNigerianPhoneNumber() != bvnDetails.PhoneNumber.AsNigerianPhoneNumber())
        {
            _logger.LogInformation($"The phone number provided does not match the phone number in the BVN");
            return null;
        }

        return new VirtualAccountOpeningRequest
        {
            BankVerificationNumber = request.BankVerificationNumber,
            Address = bvnDetails.ResidentialAddress,
            FirstName = bvnDetails.FirstName,
            LastName = bvnDetails.LastName,
            Gender = bvnDetails.Gender.ToUpper() == "FEMALE" ? "F" : "M",
            PhoneNumber = bvnDetails.PhoneNumber.AsNigerianPhoneNumber(),
            DateOfBirth = DateTime.Parse(bvnDetails.DateOfBirth),
            SecretWord = request.SecretWord,
            ReferralCode = request.ReferralCode ?? "HPP00",
            RequestId = Util.GenerateRandomNumbers(15),
            SessionId = Guid.NewGuid().ToString(),
        };

    }
    public async Task<VirtualAccountOpeningResponse> OpenVirtualAccount(CreateVirtualAccountDto request)
    {
        try
        {
            VirtualAccountOpeningRequest rubyRequest = null;
            if (string.IsNullOrEmpty(request.BankVerificationNumber))
            {
                rubyRequest = CreateVirtualAccountWithoutBvn(request);
                if (rubyRequest is null)
                {
                    return new VirtualAccountOpeningResponse { ResponseCode = "999", ResponseDescription = "Incomplete request sent", ResponseFriendlyMessage = "Incomplete request sent" };
                }
            }
            else
            {
                rubyRequest = await CreateVirtualAccountWithBvn(request);
                if (rubyRequest is null)
                {
                    return new VirtualAccountOpeningResponse { ResponseCode = "999", ResponseDescription = "Your Details does not match", ResponseFriendlyMessage = "Your Details does not match" };
                }
            }

            var rubyHeaders = new Dictionary<string, string>();

            rubyHeaders.Add("client_id", _config["RubyConnection:client_id"]);
            rubyHeaders.Add("api_key", _config["RubyConnection:api_key"]);
            rubyHeaders.Add("api_token", _config["RubyConnection:api_token"]);
            rubyHeaders.Add("organization_id", _config["RubyConnection:organization_id"]);

            var rubyResponse = await _restRequestHelper.HttpAsync(Method.POST, _config["RubyConnection:RubyUrl"], rubyHeaders, rubyRequest);

            if (string.IsNullOrEmpty(rubyResponse.Content))
            {
                _logger.LogInformation(rubyResponse.ErrorMessage);
                return new VirtualAccountOpeningResponse { ResponseCode = "999", ResponseDescription = rubyResponse.ErrorMessage, ResponseFriendlyMessage = rubyResponse.ErrorMessage };

            }

            var result = JsonConvert.DeserializeObject<VirtualAccountOpeningResponse>(rubyResponse.Content);


            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new VirtualAccountOpeningResponse { ResponseCode = "999", ResponseDescription = ex.Message, ResponseFriendlyMessage = ex.Message };
        }
    }

    private async Task<bool> SaveTierOneCustomData(string cif, CIFRequest cifRequest = null)
    {
        try
        {

            var customData = new RbxBpmCifCustomDatum
            {
                Bvn = cifRequest.CustomerBVN,
                BranchId = cifRequest.BvnEnrollmentBranch,
                MaritalStatus = cifRequest.MaritalStatus,
                EmploymentType = cifRequest.EmploymentStatus,
                NationalIdNumber = cifRequest.NIN,
                DateCreated = DateTime.UtcNow,
                CountryOfBirth = "NG",
                CountryOfTaxResidence = "NG",
                CustomerId = cif
            };
            await _modelContext.AddAsync(customData);
            //await _modelContext.SaveChangesAsync();
            var bvnLinkageLog = new RbxTBvnLinkageLog
            {
                AcctName = cifRequest.FirstName + " " + cifRequest.LastName,
                BankEnrolled = cifRequest.BvnErollmentBank,
                BranchEnrolled = cifRequest.BvnEnrollmentBranch,
                BvnNumber = cifRequest.CustomerBVN,
                PhoneNo = cifRequest.PhoneNumber,
                RecordDelFlag = "N",
                CifId = cif
            };
            await _modelContext.AddAsync(bvnLinkageLog);

            //var nextofkinId = await _modelContext.RbxBpmCifCustomData.FirstOrDefaultAsync(x => x.Bvn == cifRequest.CustomerBVN);

            var nextOfKin = new RbxBpmNextOfKinDetail
            {
                Address = cifRequest.NextOfKinDetail.Address1,
                EmailAddress = cifRequest.NextOfKinDetail.Email,
                FirstName = cifRequest.NextOfKinDetail.FirstName,
                LastName = cifRequest.NextOfKinDetail.LastName,
                PhoneNumber = cifRequest.NextOfKinDetail.PhoneNumber,
                MiddleName = cifRequest.NextOfKinDetail.MiddleName,
                DateCreated = DateTime.UtcNow,
                FkCustomerAddDetails = customData.Id,
                //FkCustomerAddDetails = nextofkinId.Id,
                FkCustomerAddDetailsNavigation = customData,
                Gender = cifRequest.Gender
            };


            await _modelContext.AddAsync(nextOfKin);

            if (await _modelContext.SaveChangesAsync() < 1)
            {
                _logger.LogInformation("There was a problem saving to the Redbox Database");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return false;
        }

    }

    private async Task<(string responseCode, string responseDescription, VerifyBVNResponseModel data)> GetBVNDetails(string bvn)
    {
        try
        {
            var request = new
            {
                requestId = Util.PaymentId(),
                bvnRequest = new
                {
                    bvns = new string[] { $"{bvn}" }
                }
            };

            var headers = new Dictionary<string, string>()
            {
                { "module_id",_config["BVN_service:moduleId"] }
            };
            var response = await _restRequestHelper.HttpAsync(Method.POST, _config["BVN_service:base_url"], headers, request);
            if (string.IsNullOrEmpty(response.Content))
            {
                _logger.LogInformation(response.ErrorMessage);
                return ("9xx", "Error validating customer's BVN", null);
            }
            var result = JsonConvert.DeserializeObject<VerifyBVNResponseModel>(response.Content);
            return (result.ResponseCode, result.ResponseMessage, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ("9xx", ex.Message, null);
        }
    }

    private async Task<(string responseCode, string responseDescription, AccountNINResponseModel data)> GetNinDetails(string nin, string dob)
    {
        var bvnDob = DateTime.Parse(dob).ToString("yyyy-MM-dd");
        try
        {
            var request = new
            {
                country = "NG",
                idNumber = nin,
                dob = bvnDob,
                channel = _config["NIN_service:channel"],
                moduleId = _config["NIN_service:moduleId"],
                idType = "NIN"
            };

            var response = await _restRequestHelper.HttpAsync(Method.POST, _config["NIN_service:base_url"], null, request);
            if (string.IsNullOrEmpty(response.Content))
            {
                _logger.LogInformation(response.ErrorMessage);
                return ("9xx", "Error validating customer's NIN", null);
            }
            var result = JsonConvert.DeserializeObject<AccountNINResponseModel>(response.Content);
            if (result.ResultCode != "1012")
            {
                return ("9xx", result.ResultText, null);
            }

            return ("00", "success", result);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
    private async Task<(string responseCode, string responseDescription, string cif)> CreateCIF(CIFRequest request)
    {
        try
        {
            var response = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.CifPayload(request));


            if (response.ResponseCode != "000")
            {
                return (response.ResponseCode, response.ResponseDescription, null);
            }

            var successDescription = _soapRequestHelper.GetXmlTagValue<string>(response.ResponseDescription, "CustId");
            if (String.IsNullOrEmpty(successDescription))
            {
                return ("999", "Finacle Responded with an error", successDescription);
            }
            return ("000", "CIF was created successfully", successDescription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ("999", "Finacle Responded with an error", ex.Message);
        }
    }

    private async Task<SaveImageResponse> SaveDocumentToDataStore(string cif, string document,string documentType)
    {
        var metadataDetails = new Dictionary<string, List<object>>();
        metadataDetails.Add("CIF Number", new List<object> { cif });
        metadataDetails.Add("Document Type", new List<object> { documentType });
        var request = new
        {
            dataDefinitionName = "Know Your Customer - (KYC)",
            metadataDefinition = metadataDetails,
            documentContent = document
        };

        var response = await _restRequestHelper.HttpAsync(Method.POST, _config["DSX_ENDPOINT"], null, request);
        if (response.Content == null)
        {
            _logger.LogInformation(response.ErrorMessage);
            return null;
        }
        var result = JsonConvert.DeserializeObject<SaveImageResponse>(response.Content);
        return result;

    }

    private async Task<bool> MobileWebOnboarding(CIFRequest request, string accountNumber)
    {
        try
        {
            var password = Encoding.UTF8.GetString(Convert.FromBase64String(request.Password));
            var confirmPassword = Encoding.UTF8.GetString(Convert.FromBase64String(request.ConfirmPassword));
            var secretQuestion = Encoding.UTF8.GetString(Convert.FromBase64String(request.SecretQuestion));
            var secretAnswer = Encoding.UTF8.GetString(Convert.FromBase64String(request.SecretAnswer));

            var onboardingRequest = new MobileWebOnboarding
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                AccountNumber = accountNumber,
                Password = password,
                ConfirmPassword = confirmPassword,
                SecretQuestion = secretQuestion,
                SecretAnswer = secretAnswer,
                Email = request.Email
            };
            var response = await _restRequestHelper.HttpAsync(Method.POST, _config["WebAndMobileOnboarding"], null, onboardingRequest);
            if (response.IsSuccessful == false)
            {
                return false;
            }
            if (response.Content == null)
            {
                _logger.LogInformation(response.ErrorMessage);
                return false;
            }
            var result = JsonConvert.DeserializeObject<MobileWebOnboardingResponse>(response.Content);
            if (result.IsSuccess)
            {
                return true;
            }
            return false;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    public async Task AddressVerificationRequest(AccountOpeningAttempt request)
    {
        var addressVerificationRequest = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.AddressVerificationRequestPayload(request.Address, request.Cif), null, _config["Address_Verification:base_url"]);

    }

    public ApiResult GetAccountNameByAccountNumber(string accountNumber)
    {
        var accountDetails = _finacleRepository.GetAccountDetailsByAccountNumber(accountNumber);
        if (accountDetails == null)
        {
            return new ApiResult { responseCode = "999", responseDescription = "Account number does not exist" };
        }

        return new ApiResult { responseCode = "000", responseDescription = $"{accountDetails.AccountName}" };
    }

    public async Task<string> UpgradeToTierThree(CIFRequest request)
    {
        var accountDetails = _finacleRepository.GetAccountDetailsByAccountNumber(request.AccountNumber);
        var upgradeDetails = new RbxRetailsUpdateCustomDatum
            {
                MonthlyNetIncome = request.MonthlyIncome.ToString(),
                // NokAddress = request.NextOfKin.Address,
                // NokAddressLine1 = request.NextOfKin.Address,
                // NokCity = request.NextOfKin.Town,
                // NokDateOfBirth = request.NextOfKin.DateOfBirth.ToString("yyyy-MM-dd"),
                // NokFirstName = request.NextOfKin.FirstName,
                // NokLastName = request.NextOfKin.LastName,
                // NokGender = request.NextOfKin.Gender,
                // NokMiddleName = request.NextOfKin.MiddleName,
                // NokPhoneNumber = request.NextOfKin.PhoneNumber,
                // Relationship = request.NextOfKin.Relationship,
                CustomerId = request.Cif,
            };
            await _modelContext.RbxRetailsUpdateCustomData.AddAsync(upgradeDetails);
            if (await _modelContext.SaveChangesAsync() < 1)
            {
                _logger.LogInformation("There was a problem saving to redbox");
                return "There was a problem uprading your account. Please try again later";
            }
            // save images

            

            
            // check address verification status
            var AddressVerificationResultCheck = await _soapRequestHelper.GetAddressVerificationStatus(
                    AccountOpeningPayloadHelper.FetchAddressVerificationReportStatusPayload(request.AddressverificationId));
            
            // check sanction screening status
            var sanctionScreeningResult = _finacleRepository.GetSanctionScreeningResult(request.SanctionScreeningAccountId);
            if (sanctionScreeningResult is null)
            {
                return null;
            }
            if (!sanctionScreeningResult.IsSuccessful)
            {
                request.Response = "This Customer did not pass Sanction Screening";
                request.AccountOpeningStatus = AccountOpeningStatus.Failed.ToString();
                await _cifRepository.UpdateCIFRequest(request.CIFRequestId,request);
                return null;
            }

            var sanctionScreeningReport = await SaveDocumentToDataStore(request.Cif,sanctionScreeningResult.Pdf,"Sanction Screening Report");

            var response = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.SchemeCodeModificationPayload(
                accountDetails.GlSubHeadCode, accountDetails.GlSubHeadCode, accountDetails.SchemeCode, "SB001", request.AccountNumber)
                );

            if (response.ResponseCode != "000")
            {
                _logger.LogInformation("There was a problem connecting to finnacle");
                return "There was a problem uprading your account. Please try again later";
            }


            var successDescription = _soapRequestHelper.GetXmlTagValue<string>(response.ResponseDescription, "CoreStatus");

            if (successDescription != "SUCCESS")
            {
                var errorDescription = _soapRequestHelper.GetXmlTagValue<string>(response.ResponseDescription, "CoreStatusDesc");
                _logger.LogInformation(errorDescription);
                return "There was a problem uprading your account. Please try again later";
            }

            return "Your account has been ugraded successfully";

    }

    public async Task<string> CheckAddressVerificationStatus(string addressVerificationId)
    {
        var status = await _soapRequestHelper.GetAddressVerificationStatus(
            AccountOpeningPayloadHelper.FetchAddressVerificationReportStatusPayload(addressVerificationId));

        return string.Empty;
    }

    public async Task<string> DownloadAddressVerificationReport(string addressVerificationId)
    {
        var response = await _soapRequestHelper.DownloadAddressVerificationReport(
            AccountOpeningPayloadHelper.DownloadAddressVerificationReport(addressVerificationId));
        
        return string.Empty;
    }


}
