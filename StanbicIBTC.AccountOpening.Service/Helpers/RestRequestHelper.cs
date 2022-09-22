using Microsoft.Extensions.Logging;
using Polly;
using RestSharp;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace StanbicIBTC.AccountOpening.Service;

public class RestRequestHelper : IRestRequestHelper
{
    private static int _maxRetryAttempts = 5;
    private static TimeSpan _pauseBetweenFailures = TimeSpan.FromSeconds(10);
    private readonly ILogger<RestRequestHelper> _logger;

    public RestRequestHelper(ILogger<RestRequestHelper> logger)
    {
        _logger = logger;
    }

    public static void SetMaxRetryAttemptsAndPauseBetweenFailures(int maxRetryAttempts, TimeSpan pauseBetweenFailures)
    {
        _maxRetryAttempts = maxRetryAttempts;
        _pauseBetweenFailures = pauseBetweenFailures;
    }

    public async Task<IRestResponse> HttpAsync(Method httpVerb, string hostUrl, Dictionary<string, string> headers, object requestObject)
    {
        var restResponse = default(IRestResponse);
        try
        {
            var restRequest = new RestRequest(httpVerb);
            restRequest.AddHeader("cache-control", "no-cache");
            if (headers != null && headers.Count > 0)
                foreach (var header in headers)
                    restRequest.AddHeader(header.Key, header.Value);

            var json = this.JsonSerialize(requestObject);
            if (httpVerb != Method.GET)
            {
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            }
            var restClient = new RestClient(hostUrl);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            //new RestClient(hostUrl)
            restResponse = this.RestResponseWithPolicy(restClient, restRequest);
            // restResponse = this.RestResponseWithPolicy(new RestClient(hostUrl), restRequest);
        }
        catch (Exception ex)
        {
            restResponse = new RestResponse
            {
                Content = ex.Message,
                ErrorMessage = ex.Message,
                ResponseStatus = ResponseStatus.TimedOut,
                StatusCode = HttpStatusCode.ServiceUnavailable
            };
        }

        return await Task.FromResult(restResponse);
    }

    private IRestResponse RestResponseWithPolicy(RestClient restClient, RestRequest restRequest)
    {
        var retryPolicy = Policy
            .HandleResult<IRestResponse>(x => !x.IsSuccessful)
            .WaitAndRetry(_maxRetryAttempts, x => _pauseBetweenFailures, (iRestResponse, timeSpan, retryCount, context) =>
            {
                _logger.LogInformation($"The request failed. HttpStatusCode={iRestResponse.Result.StatusCode}. Waiting {timeSpan} seconds before retry. Number attempt {retryCount}. Uri={iRestResponse.Result.ResponseUri}; RequestResponse={iRestResponse.Result.Content}");
            });

        var circuitBreakerPolicy = Policy
            .HandleResult<IRestResponse>(x => x.StatusCode == HttpStatusCode.ServiceUnavailable)
            .CircuitBreaker(1, TimeSpan.FromSeconds(60), onBreak: (iRestResponse, timespan, context) =>
            {
                _logger.LogInformation($"Circuit went into a fault state. Reason: {iRestResponse.Result.Content}");
            },
            onReset: (context) =>
            {
                _logger.LogInformation($"Circuit left the fault state.");
            });

        return retryPolicy.Wrap(circuitBreakerPolicy).Execute(() => restClient.Execute(restRequest));
    }

    private string JsonSerialize(object obj) => System.Text.Json.JsonSerializer.Serialize(obj, this.GetJsonSerializerOptions());

    private JsonSerializerOptions GetJsonSerializerOptions() => new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}
