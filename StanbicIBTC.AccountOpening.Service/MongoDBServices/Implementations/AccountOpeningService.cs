using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace StanbicIBTC.AccountOpening.Service;


public class AccountOpeningService : IAccountOpeningService
{
    private readonly ISoapRequestHelper _soapRequestHelper;
    private readonly ILogger<AccountOpeningService> _logger;
    private readonly IRestRequestHelper _restRequestHelper;
    private readonly IConfiguration _config;
    private readonly ICIFRequestRepository _cifRepository;
    private readonly IAccountOpeningAttemptRepository _accountOpeningAttempt;

    public AccountOpeningService(ILogger<AccountOpeningService> logger, ISoapRequestHelper soapRequestHelper, ICIFRequestRepository cifRepository, IAccountOpeningAttemptRepository accountOpeningAttempt, IConfiguration config, IRestRequestHelper restRequestHelper)
    {
        _logger = logger;
        _soapRequestHelper = soapRequestHelper;
        _cifRepository = cifRepository;
        _accountOpeningAttempt = accountOpeningAttempt;
        _config = config;
        _restRequestHelper = restRequestHelper;
    }

    public async Task<string> ValidateTierOneAccountOpeningRequest(TierOneAccountOpeningRequest request)
    {
        try
        {
            var bvnDetailsResponse = await GetBVNDetails(request.Bvn);

            if (bvnDetailsResponse.data is null)
            {
                _logger.LogInformation($"{bvnDetailsResponse.responseDescription}");
                return bvnDetailsResponse.responseDescription;
            }

            var bvnDetails = bvnDetailsResponse.data;

            var ninDetailsResponse = await GetNinDetails(request.Nin, request.DateOfBirth.ToString());

            if (ninDetailsResponse.data is null)
            {
                _logger.LogInformation($"{ninDetailsResponse.responseDescription}");
                return ninDetailsResponse.responseDescription;
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

            if (bvnDetails.PhoneNumber.AsNigerianPhoneNumber() != request.PhoneNumber)
            {
                _logger.LogInformation($"The Phone number provided is not the same with the Bvn phone number. BVN : {bvnDetails.BVN}");
                accountOpeningAttempt.Response = "The Phone number provided is not the same with the Bvn phone number";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "Details given does not match with your BVN details";
            }

            var bvnDob = DateTime.Parse(bvnDetails.DateOfBirth);
            if (bvnDob.ToString("dd-MM-yyyy") != request.DateOfBirth.ToString("dd-MM-yyyy"))
            {
                _logger.LogInformation($"BVN Date Of Birth does not match the supplied Date Of Birth");
                accountOpeningAttempt.Response = "BVN Date Of Birth does not match the supplied Date Of Birth";
                await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
                return "Details given does not match with your BVN details";
            }

            accountOpeningAttempt.Response = "Request successfully stored in the database for processing";
            var accountOpeningAttemptId = await _accountOpeningAttempt.CreateAccountOpeningAttempt(accountOpeningAttempt);
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
                MaritalStatus = request.MaritalStatus,
                LgaOfResidence = bvnDetails.LgaOfResidence,
                NIN = bvnDetails.NIN,
                PhoneNumber = bvnDetails.PhoneNumber,
                Gender = bvnDetails.Gender,
                Platform = request.Platform.ToString(),
                MiddleName = bvnDetails.MiddleName,
                NextOfKinDetail = nextOfKinDetails
            };

            var saveCifRequest = await _cifRepository.CreateCIFRequest(cifRequest);
            if (saveCifRequest == null)
            {
                _logger.LogInformation($"There was a problem saving the CIF Request for BVN: {request.Bvn}");
                return $"There was a problem saving the CIF Request for BVN: {request.Bvn} Please try again later";
            }

            return "Account request has been recieved successfully and scheduled for processing";

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ex.Message;
        }
    }

    public async Task<AccountOpeningModel> OpenAccount(CIFRequest request)
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

            var cif = await CreateCIF(request);

            if (cif.data == null)
            {
                _logger.LogInformation($"There was a problem creating CIF for this BVN : {request.CustomerBVN} ");
            }

            return new AccountOpeningModel();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
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
                IdNumber = nin,
                Dob = bvnDob,
                Channel = _config["NIN_service:channel"],
                ModuleId = _config["NIN_service:moduleId"]
            };

            var response = await _restRequestHelper.HttpAsync(Method.POST, _config["NIN_service:base_url"], null, request);
            if (response.Content == null)
            {
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
    private async Task<(string responseCode, string responseDescription, CIFCreationResponse data)> CreateCIF(CIFRequest request)
    {
        try
        {
            var response = await _soapRequestHelper.FinacleCall(CifPayload(request));

            if (response.ResponseCode != "000")
            {
                return (response.ResponseCode, response.ResponseDescription, null);
            }
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new NotImplementedException();
        }
    }

    private string CifPayload(CIFRequest request)
    {
        var dobParts = GetDateParts(request.DateOfBirthInY_M_D_Format);
        var payloaad = @$"
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
            <FIXML
                xmlns:ns5=""http://soap.finacle.redbox.stanbic.com/"" xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/"" xmlns:ns4=""http://www.finacle.com/fixml"">
                <Header>
                <RequestHeader>
                <MessageKey>
                <RequestUUID>{Util.GenerateRandomNumbers(18)}</RequestUUID>
                <ServiceRequestId>RetCustAdd</ServiceRequestId>
                <ServiceRequestVersion>10.2</ServiceRequestVersion>
                <ChannelId>RBX</ChannelId>
                </MessageKey>
                <RequestMessageInfo>
                <BankId>NG</BankId>
                <TimeZone></TimeZone>
                <EntityId></EntityId>
                <EntityType></EntityType>
                <ArmCorrelationId></ArmCorrelationId>
                <MessageDateTime>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</MessageDateTime>
                      </RequestMessageInfo>
                <Security>
                <Token>
                <PasswordToken>
                <UserId></UserId>
                <Password></Password>
                </PasswordToken>
                </Token>
                <FICertToken></FICertToken>
                <RealUserLoginSessionId></RealUserLoginSessionId>
                <RealUser></RealUser>
                <RealUserPwd></RealUserPwd>
                <SSOTransferToken></SSOTransferToken>
                </Security>
                </RequestHeader>
                </Header>
                <Body>
                <RetCustAddRequest>
                <RetCustAddRq>
                <CustDtls>
                <CustData>
                <AddrDtls>
                <AddrLine1>{request.CustomerAddress}</AddrLine1>
                <AddrCategory>Home</AddrCategory>
                <City>{request.LgaOfResidence.ToUpper().Trim()}</City>
                <Country>NG</Country>
                <FreeTextLabel>Suites</FreeTextLabel>
                <PrefAddr>Y</PrefAddr>
                <PrefFormat>FREE_TEXT_FORMAT</PrefFormat>
                <StartDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</StartDt>
                <State>{request.StateOfResidence.ToUpper().Trim()}</State>
                <PostalCode>12345</PostalCode>
                </AddrDtls>
                <AddrDtls>
                <AddrLine1>{request.CustomerAddress}</AddrLine1>
                <AddrCategory>Mailing</AddrCategory>
                <City>{request.LgaOfResidence.ToUpper().Trim()}</City>
                <Country>NG</Country>
                <FreeTextLabel>Suites</FreeTextLabel>
                <PrefAddr>N</PrefAddr>
                <PrefFormat>FREE_TEXT_FORMAT</PrefFormat>
                <StartDt>2021-09-14T05:47:35.455</StartDt>
                <State>{request.StateOfResidence.ToUpper().Trim()}</State>
                <PostalCode>12345</PostalCode>
                </AddrDtls>
                <AddrDtls>
                <AddrLine1>{request.CustomerAddress}</AddrLine1>
                <AddrCategory>Work</AddrCategory>
                <City>{request.LgaOfResidence.ToUpper().Trim()}</City>
                <Country>NG</Country>
                <FreeTextLabel>Suites</FreeTextLabel>
                <PrefAddr>N</PrefAddr>
                <PrefFormat>FREE_TEXT_FORMAT</PrefFormat>
                <StartDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</StartDt>
                <State>{request.StateOfResidence.ToUpper()}</State>
                <PostalCode>12345</PostalCode>
                </AddrDtls>
                <BirthDt>{dobParts.day}</BirthDt>
                <BirthMonth>{dobParts.month}</BirthMonth>
                <BirthYear>{dobParts.year}</BirthYear>
                <CustType>019</CustType>
                <CreatedBySystemId>1</CreatedBySystemId>
                <DateOfBirth>{dobParts.year}-{dobParts.month}-{dobParts.day}T00:00:00.000</DateOfBirth>
                <FirstName>{request.FirstName.ToUpper().Trim()}</FirstName>
                <Language>en</Language>
                <LastName>{request.LastName.ToUpper().Trim()}</LastName>
                <MiddleName>{request.MiddleName.ToUpper().Trim()}</MiddleName>
                <IsMinor>N</IsMinor>
                <IsCustNRE>N</IsCustNRE>
                <DefaultAddrType>Home</DefaultAddrType>
                <Gender>{request.Gender.ToUpper().Trim()}</Gender>
                <Manager>CCC9676</Manager>
                <Name>{request.FirstName.ToUpper()} {request.MiddleName.ToUpper()} {request.LastName.ToUpper()}</Name>
                <NativeLanguageCode>INFENG</NativeLanguageCode>
                <Occupation>009</Occupation>
                <PassportNum>A233786892</PassportNum>
                <PhoneEmailDtls>
                <PhoneEmailType>CELLPH</PhoneEmailType>
                <PhoneNum>2348062187723</PhoneNum>
                <PhoneNumCityCode>0</PhoneNumCityCode>
                <PhoneNumCountryCode>234</PhoneNumCountryCode>
                <PhoneNumLocalCode>8062187534</PhoneNumLocalCode>
                <PhoneOrEmail>PHONE</PhoneOrEmail>
                <PrefFlag>Y</PrefFlag>
                </PhoneEmailDtls>
                <PrefName>{request.FirstName.ToUpper().Trim()}</PrefName>
                <PrimarySolId>999999</PrimarySolId>
                <Region>001</Region>
                <RelationshipOpeningDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</RelationshipOpeningDt>
                <RiskProfileScore>0</RiskProfileScore>
                <Salutation>040</Salutation>
                <Sector>96</Sector>
                <SegmentationClass>001</SegmentationClass>
                <StaffEmployeeId></StaffEmployeeId>
                <StaffFlag>N</StaffFlag>
                <SubSector>S960</SubSector>
                <SubSegment>106</SubSegment>
                <TaxDeductionTable>001</TaxDeductionTable>
                <TradeFinFlag>N</TradeFinFlag>
                </CustData>
                </CustDtls>
                <RelatedDtls>
                <DemographicData>
                <EmploymentStatus>005</EmploymentStatus>
                <MaritalStatus>001</MaritalStatus>
                <Nationality>NG</Nationality>
                </DemographicData>
                <EntityDoctData>
                <CountryOfIssue>NG</CountryOfIssue>
                <DocCode>D0113</DocCode>
                <IssueDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</IssueDt>
                <TypeCode>DT010</TypeCode>
                <TypeDesc>ADDRESS PROOF INDIVIDUAL</TypeDesc>
                <PlaceOfIssue>NG</PlaceOfIssue>
                <ReferenceNum>08062187534</ReferenceNum>
                <IDIssuedOrganisation>TELEPHONE COMPANY</IDIssuedOrganisation>
                </EntityDoctData>
                <PsychographicData>
                <PsychographMiscData>
                <DTDt1>2099-12-31T00:00:00.000</DTDt1>
                <StrText10>NGN</StrText10>
                <Type>CURRENCY</Type>
                </PsychographMiscData>
                <TDSCustFloorLimit>0.0</TDSCustFloorLimit>
                </PsychographicData>
                </RelatedDtls>
                </RetCustAddRq>
                <RetCustAdd_CustomData/>
                </RetCustAddRequest>
                </Body>
                </FIXML>
                ]]>
        </arg_0_0>
        </web:executeService>
        </soapenv:Body>
        </soapenv:Envelope>
        ";
        return payloaad;
    }

    private static (string year, string month, string day) GetDateParts(string date)
    {
        var dateParts = date.Split('-');
        if (dateParts.Length == 3)
            return (dateParts[0], dateParts[1], dateParts[2]);
        return ("0001", "01", "01");
    }
}
