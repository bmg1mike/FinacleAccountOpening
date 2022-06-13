using System.Text;
using Newtonsoft.Json;
using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.BulkAccountOpeningService;

public class ApiCall : IApiCall
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public ApiCall(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
    }

    public async Task<List<BulkAccountRequest>> GetApprovedRequests()
    {
        var url = $"{_config["ApiUrl"]}GetApprovedRequests";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<List<BulkAccountRequest>>(result);
        }

        return null;
    }

    public async Task<string> OpenBulkAccounts(BulkAccountRequest requests)
    {
        var url = $"{_config["ApiUrl"]}OpenBulkAccounts";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(requests), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<string>(result);
        }

        return null;
    }
}