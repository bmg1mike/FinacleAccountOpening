using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Service;

public class SmsNotification : ISmsNotification
{
    private readonly ISoapRequestHelper _soapRequestHelper;
    private readonly ILogger<SmsNotification> _logger;
    private readonly IConfiguration _config;
    public SmsNotification(ISoapRequestHelper soapRequestHelper, ILogger<SmsNotification> logger, IConfiguration config)
    {
        _soapRequestHelper = soapRequestHelper;
        _logger = logger;
        _config = config;
    }

    public async Task SendAccountOpeningSMS(string phoneNumber, string message)
    {
        try
        {
            var result = await _soapRequestHelper.FinacleCall(SMSPayLoad(phoneNumber, message), "\"sendSmsMessage\"", _config["SMS_Service:base_url"]);

        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
        }

    }

    private string SMSPayLoad(string phoneNumber, string message)
    {
        var request = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" 
xmlns:soap=""http://soap.messaging.outbound.redbox.stanbic.com/"">
   <soapenv:Header/>
   <soapenv:Body>
      <soap:SMSRequest>
    <SMSList>
        <SMS>
            <AccountNumber>0015367487</AccountNumber>
            <ChargeCustomer>0</ChargeCustomer>
            <CostCentre>99989</CostCentre>
            <EntityCode>11289</EntityCode>
            <Message>{message}</Message>
            <RecipientList>
 		  <RecipientMobileNumber>{phoneNumber}</RecipientMobileNumber>
            </RecipientList>
            <SenderId>StanbicIBTC</SenderId>
            <UseSenderId>1</UseSenderId>
        </SMS>
    </SMSList>
      </soap:SMSRequest>
   </soapenv:Body>
</soapenv:Envelope>";

        return request;
    }
}
