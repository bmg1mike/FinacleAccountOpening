namespace StanbicIBTC.AccountOpening.Service;

public static class AccountOpeningPayloadHelper
{
    public static string CifPayload(CIFRequest request)
    {
        var dob = DateTime.Parse(request.DateOfBirthInY_M_D_Format);
        request.Gender = request.Gender.ToUpper().Trim() == "MALE" ? "M" : "F"; 
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
                <Manager>CCC9676</Manager>
                <Name>{request.FirstName.ToUpper() +" "+ request.MiddleName.ToUpper() +" "+ request.LastName.ToUpper()}</Name>
                <NativeLanguageCode>INFENG</NativeLanguageCode>
                <Occupation>{request.OccupationCode}</Occupation>
                <PassportNum>A233786892</PassportNum>
                <PhoneEmailDtls>
                <PhoneEmailType>CELLPH</PhoneEmailType>
                <PhoneNum>{request.PhoneNumber.AsNigerianPhoneNumber()}</PhoneNum>
                <PhoneNumCityCode>0</PhoneNumCityCode>
                <PhoneNumCountryCode>234</PhoneNumCountryCode>
                <PhoneNumLocalCode>{request.PhoneNumber.AsNigerianPhoneNumber().Remove(0,3)}</PhoneNumLocalCode>
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
                <ReferenceNum>{"0"+request.PhoneNumber.AsNigerianPhoneNumber().Remove(0,3)}</ReferenceNum>
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

    public static string AccountOpeningPayload(string cif,CIFRequest request)
    {
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
    <SchmCode>KYCL1</SchmCode>
    </AcctType>
    <AcctCurr>NGN</AcctCurr>
    <BankInfo>
    <BankId>NG</BankId>
    <BranchId>999999</BranchId>
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
    <SOLID>999999</SOLID>
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
}