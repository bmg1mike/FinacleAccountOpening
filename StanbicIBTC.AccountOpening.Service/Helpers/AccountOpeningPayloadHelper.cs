namespace StanbicIBTC.AccountOpening.Service;

public static class AccountOpeningPayloadHelper
{
    public static string CifPayload(CIFRequest request, string solId = "999999", string manager = "CCC9676")
    {
        var dob = DateTime.Parse(request.DateOfBirthInY_M_D_Format);
        request.Gender = request.Gender.ToUpper().Trim() == "MALE" ? "M" : "F";
        if (string.IsNullOrEmpty(request.MiddleName))
        {
            request.MiddleName = string.Empty;
        }
        if (solId is null)
            solId = "999999";

        if (manager is null)
            manager = "CCC9676";

        if (string.IsNullOrEmpty(request.MiddleName))
        {
            request.MiddleName = string.Empty;
        }

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
                <StartDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</StartDt>
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
                <BirthDt>{dob.Day}</BirthDt>
                <BirthMonth>{dob.Month}</BirthMonth>
                <BirthYear>{dob.Year}</BirthYear>
                <CustType>019</CustType>
                <CreatedBySystemId>1</CreatedBySystemId>
                <DateOfBirth>{dob.Year}-{dob.Month}-{dob.Day}T00:00:00.000</DateOfBirth>
                <FirstName>{request.FirstName.ToUpper().Trim()}</FirstName>
                <Language>en</Language>
                <LastName>{request.LastName.ToUpper().Trim()}</LastName>
                <MiddleName>{request.MiddleName.ToUpper().Trim()}</MiddleName>
                <IsMinor>N</IsMinor>
                <IsCustNRE>N</IsCustNRE>
                <DefaultAddrType>Home</DefaultAddrType>
                <Gender>{request.Gender}</Gender>
                <Manager>{manager}</Manager>
                <Name>{request.FirstName.ToUpper() + " " + request.MiddleName.ToUpper() + " " + request.LastName.ToUpper()}</Name>
                <NativeLanguageCode>INFENG</NativeLanguageCode>
                <Occupation>{request.OccupationCode}</Occupation>
                <PassportNum>A233786892</PassportNum>
                <PhoneEmailDtls>
                <PhoneEmailType>CELLPH</PhoneEmailType>
                <PhoneNum>{request.PhoneNumber.AsNigerianPhoneNumber()}</PhoneNum>
                <PhoneNumCityCode>0</PhoneNumCityCode>
                <PhoneNumCountryCode>234</PhoneNumCountryCode>
                <PhoneNumLocalCode>{request.PhoneNumber.AsNigerianPhoneNumber().Remove(0, 3)}</PhoneNumLocalCode>
                <PhoneOrEmail>PHONE</PhoneOrEmail>
                <PrefFlag>Y</PrefFlag>
                </PhoneEmailDtls>
                <PrefName>{request.FirstName.ToUpper().Trim()}</PrefName>
                <PrimarySolId>{solId}</PrimarySolId>
                <Region>001</Region>
                <RelationshipOpeningDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</RelationshipOpeningDt>
                <RiskProfileScore>0</RiskProfileScore>
                <Salutation>{request.Title}</Salutation>
                <Sector>{request.Sector ?? "96"}</Sector>
                <SegmentationClass>001</SegmentationClass>
                <StaffEmployeeId></StaffEmployeeId>
                <StaffFlag>N</StaffFlag>
                <SubSector>{request.SubSector ?? "S960"}</SubSector>
                <SubSegment>106</SubSegment>
                <TaxDeductionTable>001</TaxDeductionTable>
                <TradeFinFlag>N</TradeFinFlag>
                </CustData>
                </CustDtls>
                <RelatedDtls>
                <DemographicData>
                <EmploymentStatus>{request.EmploymentStatus}</EmploymentStatus>
                <MaritalStatus>{request.MaritalStatus}</MaritalStatus>
                <Nationality>NG</Nationality>
                </DemographicData>
                <EntityDoctData>
                <CountryOfIssue>NG</CountryOfIssue>
                <DocCode>D0113</DocCode>
                <IssueDt>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</IssueDt>
                <TypeCode>DT010</TypeCode>
                <TypeDesc>ADDRESS PROOF INDIVIDUAL</TypeDesc>
                <PlaceOfIssue>NG</PlaceOfIssue>
                <ReferenceNum>{"0" + request.PhoneNumber.AsNigerianPhoneNumber().Remove(0, 3)}</ReferenceNum>
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

    public static string AccountOpeningPayload(string cif, CIFRequest request, string schemeCode = "KYCL1", string currency = "NGN", string branchId = "999999")
    {
        if (branchId is null)
        {
            branchId = "999999";
        }
        var payloaad = @$"
    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
    <FIXML
    xmlns:ns5=""http://soap.finacle.redbox.stanbic.com/""
    xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/""
    xmlns:ns4=""http://www.finacle.com/fixml"">
    <Header>
    <RequestHeader>
    <MessageKey>
    <RequestUUID>{Util.GenerateRandomNumbers(16)}</RequestUUID>
    <ServiceRequestId>SBAcctAdd</ServiceRequestId>
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
    <SBAcctAddRequest>
    <SBAcctAddRq>
    <CustId>
    <CustId>{cif}</CustId>
    </CustId>
    <SBAcctId>
    <AcctType>
    <SchmCode>{schemeCode}</SchmCode>
    </AcctType>
    <AcctCurr>{currency}</AcctCurr>
    <BankInfo>
    <BankId>NG</BankId>
    <BranchId>{branchId}</BranchId>
    </BankInfo>
    </SBAcctId>
    <SBAcctGenInfo>
    <GenLedgerSubHead>
    <GenLedgerSubHeadCode>3501</GenLedgerSubHeadCode>
    <CurCode>NGN</CurCode>
    </GenLedgerSubHead>
    <AcctName>{request.FirstName.ToUpper() + " " + request.MiddleName.ToUpper() + " " + request.LastName.ToUpper()}</AcctName>
    <AcctShortName>{request.FirstName.ToUpper()}</AcctShortName>
    <AcctStmtMode>S</AcctStmtMode>
    <DespatchMode>N</DespatchMode>
    </SBAcctGenInfo>
    </SBAcctAddRq>
    <SBAcctAdd_CustomData>
    <INTCRACCTFLG>S</INTCRACCTFLG>
    <SOLID>{branchId}</SOLID>
    <LOCALCALFLG>N</LOCALCALFLG>
    <PRICINGCODE>PAYAS</PRICINGCODE>
    <MISENTERED>1</MISENTERED>
    <PFNUM></PFNUM>
    <EMAILTYPE></EMAILTYPE>
    <WTAXFLG>N</WTAXFLG>
    <WTAXBRNBY>N</WTAXBRNBY>
    <NXTINTRUNDT>{DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</NXTINTRUNDT>
    <DAILYCOMPINTFLG>N</DAILYCOMPINTFLG>
    <WTAXLEVELFLG>A</WTAXLEVELFLG>
    <WTAXPCNT>0</WTAXPCNT>
    <WTAXFLOORLIMIT>0</WTAXFLOORLIMIT>
    <ACCTMGRATACCT>A221040</ACCTMGRATACCT>
    </SBAcctAdd_CustomData>
    </SBAcctAddRequest>
    </Body>
    </FIXML>
    ]]>
    </arg_0_0>
    </web:executeService>
    </soapenv:Body>
    </soapenv:Envelope>";

        return payloaad;
    }

    public static string SchemeCodeModificationPayload(string newGlSubHeadCode, string oldGlSubHeadCode, string oldSchemeCode, string newSchemeCode, string accountNumber)
    {
        var payload = @$"
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
                    <FIXML xmlns:ns5=""http://soap.finacle.redbox.stanbic.com/"" xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/"" xmlns:ns4=""http://www.finacle.com/fixml"">
<Header>
<RequestHeader>
<MessageKey>
<RequestUUID>{Util.GenerateRandomNumbers(14)}</RequestUUID>
<ServiceRequestId>executeFinacleScript</ServiceRequestId>
<ServiceRequestVersion>10.2</ServiceRequestVersion>
<ChannelId>RBX</ChannelId>
</MessageKey>
<RequestMessageInfo>
<BankId>NG</BankId>
<TimeZone>
</TimeZone>
<EntityId>
</EntityId>
<EntityType>
</EntityType>
<ArmCorrelationId>
</ArmCorrelationId>
<MessageDateTime>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</MessageDateTime>
</RequestMessageInfo>
<Security>
<Token>
<PasswordToken>
<UserId>
</UserId>
<Password>
</Password>
</PasswordToken>
</Token>
<FICertToken>
</FICertToken>
<RealUserLoginSessionId>
</RealUserLoginSessionId>
<RealUser>
</RealUser>
<RealUserPwd>
</RealUserPwd>
<SSOTransferToken>
</SSOTransferToken>
</Security>
</RequestHeader>
</Header>
<Body>
<executeFinacleScriptRequest>
<ExecuteFinacleScriptInputVO>
<requestId>FI_SchmModify.scr</requestId>
</ExecuteFinacleScriptInputVO>
<executeFinacleScript_CustomData>
<acctNum>{accountNumber}</acctNum>
<oldSchm>{oldSchemeCode}</oldSchm>
<newSchm>{newSchemeCode}</newSchm>
<oldGlSubHead>{oldGlSubHeadCode}</oldGlSubHead>
<newGlSubHead>{newGlSubHeadCode}</newGlSubHead>
</executeFinacleScript_CustomData>
</executeFinacleScriptRequest>
</Body>
</FIXML>
                    ]]>
    </arg_0_0>
    </web:executeService>
    </soapenv:Body>
    </soapenv:Envelope>";

        return payload;
    }

    public static string RetailCifVerificationPayload(string cif)
    {
        var payloaad = $@"
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
        <FIXML xmlns:ns5=""http://soap.finacle.redbox.stanbic.com/"" xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/"" xmlns:ns4=""http://www.finacle.com/fixml"">
<Header>
<RequestHeader>
<MessageKey>
<RequestUUID>2104212152533717</RequestUUID>
<ServiceRequestId>verifyCustomerDetails</ServiceRequestId>
<ServiceRequestVersion>10.2</ServiceRequestVersion>
<ChannelId>RBX</ChannelId>
</MessageKey>
<RequestMessageInfo>
<BankId>NG</BankId>
<TimeZone>
</TimeZone>
<EntityId>
</EntityId>
<EntityType>
</EntityType>
<ArmCorrelationId>
</ArmCorrelationId>
<MessageDateTime>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</MessageDateTime>
</RequestMessageInfo>
<Security>
<Token>
<PasswordToken>
<UserId>
</UserId>
<Password>
</Password>
</PasswordToken>
</Token>
<FICertToken>
</FICertToken>
<RealUserLoginSessionId>
</RealUserLoginSessionId>
<RealUser>
</RealUser>
<RealUserPwd>
</RealUserPwd>
<SSOTransferToken>
</SSOTransferToken>
</Security>
</RequestHeader>
</Header>
<Body>
<verifyCustomerDetailsRequest>
<CustomerVerifyRq>
<cifId>{cif}</cifId>
<decision>Approve</decision>
<entityName>RetailCustomer</entityName>
</CustomerVerifyRq>
</verifyCustomerDetailsRequest>
</Body>
</FIXML>
 ]]>
    </arg_0_0>
    </web:executeService>
    </soapenv:Body>
    </soapenv:Envelope>";
        return payloaad;
    }

    public static string CorporateCifVerificationPayload(string cif)
    {
        var payload = @$"
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
        <FIXML xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/"" xmlns:ns4=""http://www.finacle.com/fixml"">
<Header>
<RequestHeader>
<MessageKey>
<RequestUUID>2103071741411043</RequestUUID>
<ServiceRequestId>verifyCustomerDetails</ServiceRequestId>
<ServiceRequestVersion>10.2</ServiceRequestVersion>
<ChannelId>RBX</ChannelId>
</MessageKey>
<RequestMessageInfo>
<BankId>NG</BankId>
<TimeZone>
</TimeZone>
<EntityId>
</EntityId>
<EntityType>
</EntityType>
<ArmCorrelationId>
</ArmCorrelationId>
<MessageDateTime>{DateTime.Now.ToString("yyyy-MM-dd" + "T" + "HH:mm:ss.fff")}</MessageDateTime>
</RequestMessageInfo>
<Security>
<Token>
<PasswordToken>
<UserId>
</UserId>
<Password>
</Password>
</PasswordToken>
</Token>
<FICertToken>
</FICertToken>
<RealUserLoginSessionId>
</RealUserLoginSessionId>
<RealUser>
</RealUser>
<RealUserPwd>
</RealUserPwd>
<SSOTransferToken>
</SSOTransferToken>
</Security>
</RequestHeader>
</Header>
<Body>
<verifyCustomerDetailsRequest>
<CustomerVerifyRq>
<cifId>{cif}</cifId>
<decision>Approve</decision>
<entityName>CorporateCustomer</entityName>
</CustomerVerifyRq>
</verifyCustomerDetailsRequest>
</Body>
</FIXML>
 ]]>
    </arg_0_0>
    </web:executeService>
    </soapenv:Body>
    </soapenv:Envelope>";

        return payload;
    }

    public static string AddressVerificationRequestPayload(string Address, string cif)
    {
        var payload = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header />
               <soapenv:Body>
                  <tem:LogAddressVerificationRequest>
                     <tem:Cif_Id>{cif}</tem:Cif_Id>
                    <tem:RequestType>1</t8em:RequestType>
                     <tem:Alias>neon</tem:Alias>
                    <tem:Branchid>000001</tem:Branchid>
                    <tem:LandMark>Conoil Gas Station</tem:LandMark>
                     <tem:LoggedBy>A168357</tem:LoggedBy>
                  </tem:LogAddressVerificationRequest>
               </soapenv:Body>
            </soapenv:Envelope>";
        return payload;
    }

    public static string FetchAddressVerificationReportStatusPayload(string requestId)
    {
        var payload = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
            <soapenv:Header/>
            <soapenv:Body>
                <tem:GetCompleteAddressStatus>
                    <tem:RequestID>{requestId}</tem:RequestID>
                </tem:GetCompleteAddressStatus>
            </soapenv:Body>
            </soapenv:Envelope>";
        return payload;
    }

    public static string DownloadAddressVerificationReport(string requestId)
    {
        var payload = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
            <soapenv:Header/>
            <soapenv:Body>
                <tem:DownloadPDFInBase64String>
                    <tem:RequestID>{requestId}</tem:RequestID>
                </tem:DownloadPDFInBase64String>
            </soapenv:Body>
            </soapenv:Envelope>";
        return payload;
    }

    public static string CorporateAccountCifPayload()
    {
        var payload = $@"
         <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.fiusb.ci.infosys.com/"">
    <soapenv:Header/>
    <soapenv:Body>
        <web:executeService>
            <arg_0_0>
                <![CDATA[
                    <FIXML xmlns:ns5=""http://soap.finacle.redbox.stanbic.com/"" xmlns:ns2=""http://webservice.fiusb.ci.infosys.com/"" xmlns:ns4=""http://www.finacle.com/fixml"">
<Header>
<RequestHeader>
<MessageKey>
<RequestUUID>28Aug2018corpAdd002</RequestUUID>
<ServiceRequestId>createCorporateCustomer</ServiceRequestId>
<ServiceRequestVersion>10.2</ServiceRequestVersion>
<ChannelId>RBX</ChannelId>
</MessageKey>
<RequestMessageInfo>
<BankId>NG</BankId>
<TimeZone>
</TimeZone>
<EntityId>
</EntityId>
<EntityType>
</EntityType>
<ArmCorrelationId>
</ArmCorrelationId>
<MessageDateTime>2021-02-23T06:39:03.671</MessageDateTime>
</RequestMessageInfo>
<Security>
<Token>
<PasswordToken>
<UserId>
</UserId>
<Password>
</Password>
</PasswordToken>
</Token>
<FICertToken>
</FICertToken>
<RealUserLoginSessionId>
</RealUserLoginSessionId>
<RealUser>
</RealUser>
<RealUserPwd>
</RealUserPwd>
<SSOTransferToken>
</SSOTransferToken>
</Security>
</RequestHeader>
</Header>
<Body>
<createCorporateCustomerRequest>
<createCorporateCustomer_CustomData>
<corpMiscInfo.0.CRNCY_CODE>NGN</corpMiscInfo.0.CRNCY_CODE>
</createCorporateCustomer_CustomData>
<CorpCustDetails>
<corpCustomerDtlsData>
<corpCustomerData>
<segment>005</segment>
<primaryRM_ID>A171462</primaryRM_ID>
<corporate_name>TEST INC</corporate_name>
<nativeLangCode>INFENG</nativeLangCode>
<date_of_incorporation>2007-09-26T00:00:00.000</date_of_incorporation>
<trade_services_availed>N</trade_services_availed>
<crncy_Code>NGN</crncy_Code>
<CustTypeDesc>006</CustTypeDesc> check
<priority>Normal</priority>
<corpPhoneEmailData>
<phonenolocalcode>8066457858</phonenolocalcode>
<phoneoremail>PHONE</phoneoremail>
<preferredflag>Y</preferredflag>
<phoneemailtype>CELLPH</phoneemailtype>
<phonenocountrycode>234</phonenocountrycode>
<phonenocitycode>0</phonenocitycode>
<phoneno>2348066457858</phoneno>
</corpPhoneEmailData>
<corpPhoneEmailData>
<email>info@test.com</email>
<phoneoremail>EMAIL</phoneoremail>
<preferredflag>Y</preferredflag>
<phoneemailtype>REGEML</phoneemailtype>
</corpPhoneEmailData>
<relationship_createdby>DUMMY</relationship_createdby>
<subsegment>301</subsegment>
<islamic_banking_customer>N</islamic_banking_customer>
<cust_hlth>003</cust_hlth>
<health_desc>003</health_desc>
<corpAddressData>
<zip>234</zip>
<state>LAGOS</state>
<addresscategory>Registered</addresscategory>
<Country>NG</Country>
<preferredAddress>Y</preferredAddress>
<City>LAGOS</City>
<preferredFormat>FREE_TEXT_FORMAT</preferredFormat>
<address_line1>54-56</address_line1>
<address_line2>Marshalltown Mapungunbwe</address_line2>
<address_line3>
</address_line3>
<FreeTextLabel>.</FreeTextLabel> Check this----------------------------
<start_date>2018-08-09T00:00:00.000</start_date>
<holdMailFlag>N</holdMailFlag>
<street_name>
</street_name>
</corpAddressData>
<lang_desc>en</lang_desc>
<relationship_startdate>2017-09-27T12:20:21.621</relationship_startdate>
<short_name>TEST INC</short_name>
<createdBySystemID>TN014855</createdBySystemID> check this
<average_annualincome>1</average_annualincome>
<defaultaddresstype>Registered</defaultaddresstype>
<primaryRMLOGIN_ID>A169189</primaryRMLOGIN_ID>
<startdate>2018-08-09T12:20:21.621</startdate>
<primary_service_center>000191</primary_service_center>
<entity_type>Customer</entity_type>
<cust_type_desc>003</cust_type_desc>
<registration_number>RC122666</registration_number>
<business_Type>005</business_Type>
</corpCustomerData>
</corpCustomerDtlsData>
<corpRelatedData>
<preferencesData>
<corpPrefMiscData>
<str1>USD</str1>
<str10>CURRENCY</str10>
<type>CURRENCY</type>
<date1>2099-12-31T00:00:00.000</date1>
</corpPrefMiscData>
<corpPrefMiscData>
<str1>GBP</str1>
<str10>CURRENCY</str10>
<type>CURRENCY</type>
<date1>2099-12-31T00:00:00.000</date1>
</corpPrefMiscData>
<corpPrefMiscData>
<str1>EUR</str1>
<str10>CURRENCY</str10>
<type>CURRENCY</type>
<date1>2099-12-31T00:00:00.000</date1>
</corpPrefMiscData>
<corpPrefMiscData>
<str1>NGN</str1>
<str10>CURRENCY</str10>
<type>CURRENCY</type>
<date1>2099-12-31T00:00:00.000</date1>
</corpPrefMiscData>
</preferencesData>
<corpEntityDocumentData/>
<corpEntityDocumentData>
<docissuedate>2010-10-27T00:00:00.000</docissuedate>
<doctypecode>DT000</doctypecode>
<placeofissue>NG</placeofissue>
<countryofissue>NG</countryofissue>
<referencenumber>UPLOAD</referencenumber>
<docexpirydate>2099-01-01T00:00:00.000</docexpirydate>
<doccode>D0002</doccode>
<preferredUniqueId>UPLOAD</preferredUniqueId>
</corpEntityDocumentData>
<corpEntityDocumentData>
<docissuedate>2010-10-27T00:00:00.000</docissuedate>
<doctypecode>DT000</doctypecode>
<placeofissue>NG</placeofissue>
<countryofissue>NG</countryofissue>
<referencenumber>UTILITY BILL OF SIGNATORIES</referencenumber>
<docexpirydate>2099-01-01T00:00:00.000</docexpirydate>
<doccode>D0004</doccode>
<preferredUniqueId>UTILITY BILL OF SIGNATORIES</preferredUniqueId>
</corpEntityDocumentData>
</corpRelatedData>
</CorpCustDetails>
</createCorporateCustomerRequest>
</Body>
</FIXML>
]]>
    </arg_0_0>
    </web:executeService>
    </soapenv:Body>
    </soapenv:Envelope>";

        return payload;
    }

    public static string DedupCheckPayload(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
    {
        var payload = @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:soap=""http://soap.request.manager.redbox.stanbic.com/"">
<soapenv:Header/>
<soapenv:Body>
<ns2:request xmlns:ns2=""http://soap.request.manager.redbox.stanbic.com/"">
<channel>BPM</channel>
<type>DUPE_CHECK</type>
<body><![CDATA[<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<otherRequestDetails>
<customerType>
</customerType>
<firstName>{firstName}</firstName>
<middleName></middleName>
<lastName>{lastName}</lastName>
<dOB>1993-10-03T00:00:00.000</dOB>{dateOfBirth.ToString("YYYY-MM-DD")}
<phoneNumber>{phoneNumber.AsNigerianPhoneNumber()}</phoneNumber>
<corporateName>
</corporateName>
<rCNumber>
</rCNumber>
</otherRequestDetails>]]></body>
</ns2:request>
</soapenv:Body>
</soapenv:Envelope>";
        return payload;
    }
}
