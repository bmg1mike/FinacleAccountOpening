using System.Text;
using Newtonsoft.Json;
using StanbicIBTC.AccountOpening.Domain;
using StanbicIBTC.AccountOpening.Service;

namespace StanbicIBTC.AccountOpening.BulkAccountOpeningService;

public class ApiCall : IApiCall
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly IBulkAccountOpeningService _service;

    public ApiCall(HttpClient client, IConfiguration config, IBulkAccountOpeningService service)
    {
        _client = client;
        _config = config;
        _service = service;
    }

    public async Task<List<BulkAccountRequest>> GetApprovedRequests()
    {
        var requests = await _service.GetApprovedRequests();
        //var url = $"{_config["ApiUrl"]}GetApprovedRequests";
        //var request = new HttpRequestMessage(HttpMethod.Get, url);
        //var response = await _client.SendAsync(request);
        //var result = await response.Content.ReadAsStringAsync();

        //if (response.IsSuccessStatusCode)
        //{
        //    return JsonConvert.DeserializeObject<List<BulkAccountRequest>>(result);
        //}

        return requests;
    }

    public async Task<string> OpenBulkAccounts(BulkAccountRequest requests)
    {
        var accounts = await _service.OpenBulkAccounts(requests);
        //var url = $"{_config["ApiUrl"]}OpenBulkAccounts";
        //var request = new HttpRequestMessage(HttpMethod.Post, url);
        //request.Content = new StringContent(JsonConvert.SerializeObject(requests), Encoding.UTF8, "application/json");
        //var response = await _client.SendAsync(request);
        //var result = await response.Content.ReadAsStringAsync();

        //if (response.IsSuccessStatusCode)
        //{
        //    return JsonConvert.DeserializeObject<string>(result);
        //}

        return accounts;
    }
}