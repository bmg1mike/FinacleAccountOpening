using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Service;

public class MassageNotification : IMassageNotification
{
    private readonly HttpClient _client;
    private readonly ILogger<MassageNotification> _logger;
    private readonly IConfiguration _config;
    public MassageNotification(ILogger<MassageNotification> logger, IConfiguration config, HttpClient client)
    {
        _logger = logger;
        _config = config;
        _client = client;
    }

    public async Task<bool> SendAccountOpeningSMS(SMSRequest request)
    {
        try
        {
            var url = $"{_config["Messaging_Service:base_url"]}/send-sms";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post,url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",_config["Messaging_Service:Auth"]);
            requestMessage.Headers.Add("ModuleId",_config["Messaging_Service:ModuleId"]);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");
            var response = await _client.SendAsync(requestMessage);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            _logger.LogInformation(result);
            return false;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
            
        }

    }

}
