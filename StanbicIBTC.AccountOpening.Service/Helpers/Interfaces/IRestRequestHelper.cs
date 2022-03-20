using RestSharp;

namespace StanbicIBTC.AccountOpening.Service;

public interface IRestRequestHelper
{
    Task<IRestResponse> HttpAsync(Method httpVerb, string hostUrl, Dictionary<string, string> headers, object requestObject);
}
