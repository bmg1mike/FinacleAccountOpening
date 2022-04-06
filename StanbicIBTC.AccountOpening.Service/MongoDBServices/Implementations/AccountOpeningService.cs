using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using StanbicIBTC.AccountOpening.Data;

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
    private readonly ModelContext _modelContext;

    public AccountOpeningService(ILogger<AccountOpeningService> logger, ISoapRequestHelper soapRequestHelper, ICIFRequestRepository cifRepository, IAccountOpeningAttemptRepository accountOpeningAttempt, IConfiguration config, IRestRequestHelper restRequestHelper, IFinacleRepository finacleRepository, ISmsNotification smsNotification, ModelContext modelContext)
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
    }

    public async Task<ApiResult> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request)
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

            var ninDetailsResponse = await GetNinDetails(request.Nin, request.DateOfBirth.ToString());

            if (ninDetailsResponse.data is null)
            {
                _logger.LogInformation($"{ninDetailsResponse.responseDescription}");
                return new ApiResult { responseCode = "999", responseDescription = ninDetailsResponse.responseDescription };
            }

            var ninDetails = ninDetailsResponse.data;

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

            
            var nextOfKinDetails = new CIFNextOfKinDetail
            {
                FirstName = ninDetails.FullData.nok_firstname,
                LastName = ninDetails.FullData.nok_lastname,
                Address1 = ninDetails.FullData.nok_address1,
                Address2 = ninDetails.FullData.nok_address2,
                State = ninDetails.FullData.nok_state,
                Town = ninDetails.FullData.nok_town
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

            //var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            //if (saveCifRequest == null)
            //{
            //    _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
            //    await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again later");
            //    return new ApiResult { responseCode = "999", responseDescription = $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later" };
            //}



            //accountOpeningAttempt.Response = "Request successfully stored in the database for processing";
            //var accountOpeningAttemptId = await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
            var accountNumber = await OpenAccount(cifRequest);
            if (string.IsNullOrEmpty(accountNumber))
            {
                return new ApiResult { responseCode = "999", responseDescription = $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later" };
            }

            if (request.WillOnboard && (request.Platform == Platform.WEB || request.Platform == Platform.MOBILE))
            {
                if (string.IsNullOrEmpty(request.SecretQuestion) && string.IsNullOrEmpty(request.SecretAnswer) && string.IsNullOrEmpty(request.Password))
                {
                    return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Account Number Is {accountNumber}. Onboarding Failed for lack of adequate information " };
                }

                var isOnboarded = await MobileWebOnboarding(request, bvnDetails,accountNumber);
                if (isOnboarded)
                {
                    return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Account Number Is {accountNumber}. You can now use the Mobile App and Internet banking" };
                }

            }

            return new ApiResult { responseCode = "000", responseDescription = $"Your Account has been opened Successfully. Your Account Number Is {accountNumber}." };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ApiResult { responseCode = "999", responseDescription=ex.Message };
        }
    }

    

    public async Task UpgradeToTierThreeAccountOpening(TierOneUgrade request)
    {
        try
        {
            // check account number 
            
            var upgradeDetails = new RbxRetailsUpdateCustomDatum
            {
                MonthlyNetIncome = request.MonthlyIncome.ToString(),
                NokAddress = request.NextOfKin.Address,
                NokAddressLine1 = request.NextOfKin.Address,
                NokCity = request.NextOfKin.Town,
                NokDateOfBirth = request.NextOfKin.DateOfBirth.ToString("yyyy-MM-dd"),
                NokFirstName = request.NextOfKin.FirstName,
                NokLastName = request.NextOfKin.LastName,
                NokGender = request.NextOfKin.Gender,
                NokMiddleName = request.NextOfKin.MiddleName,
                NokPhoneNumber = request.NextOfKin.PhoneNumber,
                Relationship = request.NextOfKin.Relationship 
            };
        }
        catch (Exception ex)
        {

            throw;
        }
        throw new NotImplementedException();
        // check account and convert to tier 3

    }
    public async Task OpenChessAccount()
    {
        throw new NotImplementedException();
    }

    public async Task OpenCurrentAccout()
    {
        throw new NotImplementedException();
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
                    
                }
            };
            // Check if CIF exist

            var createCif = await CreateCIF(cifRequest);
            if (createCif.cif == null)
            {
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                _logger.LogInformation($"There was a problem creating CIF for this BVN : {request.Bvn} ");
                return new ApiResult { responseCode = "999", responseDescription = "We are unable to complete the account opening process, please try again" };
            }

            // save all images
            var saveIdImage = await SaveImage(createCif.cif,request.IdImage);
            var savePhotograph = await SaveImage(createCif.cif,request.PassportPhotograph);

            var saveToRedbox = await SaveToRedbox(createCif.cif,cifRequest,request);

            var openAccount = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.AccountOpeningPayload(createCif.cif, cifRequest, "SB001",request.CurrencyCode.ToUpper()));

            if (openAccount.ResponseCode != "000")
            {
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                accountOpeningAttempt.Response = $"Could not open account for CIF : {createCif.cif}";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return null;
            }


            var accountNumber = _soapRequestHelper.GetXmlTagValue<string>(openAccount.ResponseDescription, "AcctId");
            if (string.IsNullOrEmpty(accountNumber))
            {
                accountOpeningAttempt.Response = $"Could not open account for CIF : {createCif.cif}";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return new ApiResult { responseCode = "999", responseDescription = "Could not open the account, please try again later " };
            }

            accountOpeningAttempt.Response = "Account openned successfully";
            accountOpeningAttempt.IsSuccessful = true;
            await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
            return new ApiResult { responseCode = "000", responseDescription = $"Your Tier 3 Account number is :{accountNumber}" };


        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
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
            var accountOpeningAttempt = new AccountOpeningAttempt
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Bvn = request.CustomerBVN,
                Response = string.Empty,
                PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber()
            };

            request.StateOfResidence = _finacleRepository.GetStateCode(request.StateOfResidence.ToUpper().Replace("STATE", "").Trim());
            if (string.IsNullOrEmpty(request.StateOfResidence))
            {
                accountOpeningAttempt.Response = "The state in the BVN is not available in the finacle Database";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                return null;
            }

            request.LgaOfResidence = _finacleRepository.GetCityCode(request.LgaOfResidence.ToUpper().Trim());
            if (string.IsNullOrEmpty(request.LgaOfResidence))
            {
                accountOpeningAttempt.Response = "The City in the BVN is not available in the finacle Database";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                return null;
            }

            

            var cif = await CreateCIF(request);

            if (cif.cif == null)
            {
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                _logger.LogInformation($"There was a problem creating CIF for this BVN : {request.CustomerBVN} ");
            }

            //var redboxResult = await SaveToRedbox(cif.cif,request);
            //if (!redboxResult)
            //{
            //    accountOpeningAttempt.Response = "There was a problem saving the request to the redbox database";
            //    await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
            //    await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
            //    return ($"There was a problem saving the CIF Request for BVN: {request.CustomerBVN} Please try again later");
            //}

            var openSavingsAccount = await _soapRequestHelper.FinacleCall(AccountOpeningPayloadHelper.AccountOpeningPayload(cif.cif, request));

            if (openSavingsAccount.ResponseCode != "000")
            {
                await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, "We are unable to complete the account opening process, please try again");
                accountOpeningAttempt.Response = $"Could not open account for CIF : {cif.cif}";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return null;
            }


            var successDescription = _soapRequestHelper.GetXmlTagValue<string>(openSavingsAccount.ResponseDescription, "AcctId");
            if (string.IsNullOrEmpty(successDescription))
            {
                accountOpeningAttempt.Response = $"Could not open account for CIF : {cif.cif}";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return $"Could not open the account for CIF : {cif.cif}";
            }
            var cifRequest = await _cifRepository.GetCIFRequest(request.CIFRequestId);
            cifRequest.AccountOpeningStatus = AccountOpeningStatus.Completed.ToString();
            await _cifRepository.UpdateCIFRequest(cifRequest.CIFRequestId, cifRequest);
            var successMessage = $"Congratulations! Your account has been opened using your BVN details, Account Number ({successDescription}). \n To get activated on our channels please visit https://ibanking.stanbicibtcbank.com/quickservices or our nearest branch.";
            await _smsNotification.SendAccountOpeningSMS(request.PhoneNumber, successMessage);
            return $"Account Number is {successDescription}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    private VirtualAccountOpeningRequest CreateVirtualAccountWithoutBvn(CreateVirtualAccountDto request)
    {
        if (string.IsNullOrEmpty(request.FirstName) && string.IsNullOrEmpty(request.LastName ) && string.IsNullOrEmpty(request.Gender)
            && string.IsNullOrEmpty(request.Address) && string.IsNullOrEmpty(request.PhoneNumber) && 
            string.IsNullOrEmpty(request.SecretWord) )
        {
            return null;
        }
        return new VirtualAccountOpeningRequest
            {
                BankVerificationNumber = "",
                Address = request.Address,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender.ToUpper() == "FEMALE" ? "F" : "M",
                PhoneNumber = request.PhoneNumber.AsNigerianPhoneNumber(),
                DateOfBirth = request.DateOfBirth,
                SecretWord = request.SecretWord,
                ReferralCode = request.ReferralCode ?? "HPP00",
                RequestId = Util.GenerateRandomNumbers(15),
                SessionId = Guid.NewGuid().ToString(),
            };
    } 

    private async Task<VirtualAccountOpeningRequest>CreateVirtualAccountWithBvn(CreateVirtualAccountDto request)
    {
        var bvnDetailsResponse = await GetBVNDetails(request.BankVerificationNumber);

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                return null;
            }

            var bvnDetails = bvnDetailsResponse.data;

            if (request.PhoneNumber.AsNigerianPhoneNumber() != request.PhoneNumber.AsNigerianPhoneNumber())
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
                    return new VirtualAccountOpeningResponse{ ResponseCode = "999", ResponseDescription = "Incomplete request sent", ResponseFriendlyMessage = "Incomplete request sent"};
                }
            }
            else
            {
                rubyRequest = await CreateVirtualAccountWithBvn(request);
                if (rubyRequest is null)
                {
                    return new VirtualAccountOpeningResponse{ ResponseCode = "999", ResponseDescription = "Incomplete request sent", ResponseFriendlyMessage = "Incomplete request sent"};
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
        catch ( Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new VirtualAccountOpeningResponse { ResponseCode = "999", ResponseDescription = ex.Message, ResponseFriendlyMessage = ex.Message };
        }
    }

    private async Task<bool> SaveToRedbox(string cif, CIFRequest cifRequest = null, TierThreeAccountOpeningRequest tierThreeAccount = null )
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
                TaxIdentificationNumber = tierThreeAccount.TaxIdentificationNumber,
                TaxIdNumber = tierThreeAccount.TaxIdentificationNumber,
                IdentityNumber = tierThreeAccount.IdNumber,
                MonthlyNetIncome = tierThreeAccount.MonthlyIncome.ToString(),
                IdentityType = tierThreeAccount.IdentityType,
                CustomerId = cif,
                
            };
            await _modelContext.AddAsync(customData);
            var result = await _modelContext.SaveChangesAsync();



            var getCustomer = await _modelContext.RbxBpmCifCustomData.FirstOrDefaultAsync(x => x.Bvn == cifRequest.CustomerBVN);
            var nextOfKin = new RbxBpmNextOfKinDetail
            {
                Address = cifRequest.NextOfKinDetail.Address1,
                EmailAddress = cifRequest.NextOfKinDetail.Email,
                FirstName = cifRequest.NextOfKinDetail.FirstName,
                LastName = cifRequest.NextOfKinDetail.LastName,
                PhoneNumber = cifRequest.NextOfKinDetail.PhoneNumber,
                MiddleName = cifRequest.NextOfKinDetail.MiddleName,
                DateCreated = DateTime.UtcNow,
                FkCustomerAddDetails = getCustomer.Id,
                FkCustomerAddDetailsNavigation = getCustomer,
                Gender = cifRequest.Gender,
                DateOfBirth = tierThreeAccount.NextOfKinDetails.DateOfBirth,
                RelationshipType = tierThreeAccount.NextOfKinDetails.Relationship
            };

            
            await _modelContext.AddAsync(nextOfKin);

            if (await _modelContext.SaveChangesAsync() <= 0)
            {
                _logger.LogInformation("There was a problem saving to the Redbox Database");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);

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
            // validate BVN
            var headers = new Dictionary<string, string>()
            {
                { "module_id",_config["BVN_service:moduleId"] }
            };
            var response = await _restRequestHelper.HttpAsync(Method.POST, _config["BVN_service:base_url"], headers, request);
            if (response.Content is null)
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
            if (response.Content == null)
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
            //var response = await _soapRequestHelper.FinacleCall(TestPayload(request));

            if (response.ResponseCode != "000")
            {
                return (response.ResponseCode, response.ResponseDescription, null);
            }

            var successDescription = _soapRequestHelper.GetXmlTagValue<string>(response.ResponseDescription,"CustId");
            if (String.IsNullOrEmpty(successDescription))
            {
                return ("999","Finacle Responded with an error",successDescription);
            }
            return ("000","CIF was created successfully", successDescription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ("999","Finacle Responded with an error",ex.Message);
        }
    }

    private async Task<SaveImageResponse> SaveImage(string cif, string image)
    {
        var metadataDetails = new Dictionary<string, List<object>>();
        metadataDetails.Add("CIF Number", new List<object> { cif });
        metadataDetails.Add("Document Type", new List<object> { "Account Administration" });
        var request = new
        {
            dataDefinitionName = "Know Your Customer - (KYC)",
            metadataDefinition = metadataDetails,
            documentContent = image
        };

        var response = await _restRequestHelper.HttpAsync(Method.POST, _config["DSX_ENDPOINT"], null, request);
        if (response.Content == null)
        {
            _logger.LogInformation(response.ErrorMessage);
            return  null;
        }
        var result = JsonConvert.DeserializeObject<SaveImageResponse>(response.Content);
        return result;

    }

    private async Task<bool> MobileWebOnboarding(TierOneAccountOpeningRequest request, VerifyBVNResponseModel bvnDetails, string accountNumber)
    {
        try
        {
            var onboardingRequest = new MobileWebOnboarding
            {
                FirstName = bvnDetails.FirstName,
                LastName = bvnDetails.LastName,
                PhoneNumber = bvnDetails.PhoneNumber,
                AccountNumber = accountNumber,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                SecretQuestion = request.SecretQuestion,
                SecretAnswer = request.SecretAnswer,
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
            _logger.LogError(ex,ex.Message);
            return false;
        }
    }

    private static (string year, string month, string day) GetDateParts(string date)
    {
        var dateParts = date.Split('-');
        if (dateParts.Length == 3)
            return (dateParts[0], dateParts[1], dateParts[2]);
        return ("0001", "01", "01");
    }

    

       
}
