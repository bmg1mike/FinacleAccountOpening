using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace StanbicIBTC.AccountOpening.Service;

public class SoapRequestHelper : ISoapRequestHelper
{
    private readonly ILogger<SoapRequestHelper> _logger;
    private readonly IConfiguration _configSettings;
    private readonly HttpClient _client;

    public SoapRequestHelper(ILogger<SoapRequestHelper> logger, IConfiguration settings, HttpClient client)
    {
        _logger = logger;
        _configSettings = settings;
        _client = client;
    }

    public async Task<SoapCallResponse> FinacleCall(string soapRequest, string soapAction = "\"treat\"", string url = "", string moduleId = "", string authId = "")
    {
        url = string.IsNullOrEmpty(url) ? _configSettings["Finacle:base_url"] : url;
        moduleId = string.IsNullOrEmpty(moduleId) ? _configSettings["Finacle:moduleId"] : moduleId;
        authId = string.IsNullOrEmpty(authId) ? _configSettings["Finacle:authorization"] : authId;

        

        var responseResult = new SoapCallResponse("99", "Init");
        var reqId = $"{soapAction}_{Util.TimeStampCode()}";
        _logger.LogInformation($"{soapAction} API REQ: {reqId}\nModuleId:{moduleId}|AuthId:{authId}\n{soapRequest}:{url}");
        try
        {
            var uri = new Uri(url);
            var baseurl = uri.Scheme + "://" + uri.Authority;
            var path = uri.PathAndQuery;

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += ValidateServerCertificate;

            using var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseurl),
                Timeout = TimeSpan.FromHours(1)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            //client.DefaultRequestHeaders.Add("SOAPAction", soapAction);
            client.DefaultRequestHeaders.Add("module_id", $"{moduleId}");
            client.DefaultRequestHeaders.Add("authorization", $"{authId}");
            HttpResponseMessage responseMessage = null;
            _logger.LogInformation("Request For Finacle : {@soapRequest}", soapRequest);
            HttpContent contentPost = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            responseMessage = await client.PostAsync(url, contentPost);
            if (responseMessage.IsSuccessStatusCode)
            {
                var resp = await responseMessage.Content.ReadAsStringAsync();
                responseResult = new SoapCallResponse("000", resp);
            }
            else
            {
                var resp = await responseMessage.Content.ReadAsStringAsync();
                responseResult = new SoapCallResponse("9XX", resp);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            responseResult = new SoapCallResponse("9XX", ex.Message);
        }

        var responseLogMsg = $"{soapAction} API RESP: {reqId} -> {JsonConvert.SerializeObject(responseResult)}";
        _logger.LogInformation(responseLogMsg);
        return responseResult;
    }

    private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return "0".Trim().Length < 5;
    }

    public T GetXmlTagValue<T>(string xmlObject, string element, bool withTag = false, bool deserialize = false, string namespacePrefix = "", bool ignoreCase = true)
    {
        try
        {
            xmlObject = xmlObject.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&lt;", "<").Replace("&gt;", ">").Replace("< ", "<").Replace(" >", ">").Replace(" />", "/>").Replace("</ ", "</");
            var pattern = string.IsNullOrEmpty(namespacePrefix) ? $@"<{element}>.+?</{element}>" : $@"<{element}[^>].+</{element}>";
            var matches = ignoreCase ? Regex.Matches(xmlObject, pattern, RegexOptions.IgnoreCase) : Regex.Matches(xmlObject, pattern);
            var matchCount = matches.Count;
            if (matchCount < 1)
                return (T)(object)"";

            var value = matches[0].Value;
            if (!withTag)
            {
                var openingTag = string.IsNullOrEmpty(namespacePrefix) ? $"<{element}>" : $"<{namespacePrefix}:{element}>";
                var closingTag = string.IsNullOrEmpty(namespacePrefix) ? $"</{element}>" : $"</{namespacePrefix}:{element}>";

                value = value.ToString().Replace(openingTag, "").Replace(closingTag, "")?.Trim();
            }

            if (deserialize)
            {
                if (!ignoreCase)
                {
                    var xDoc = new XmlDocument();
                    xDoc.LoadXml(value);
                    foreach (XmlNode name in xDoc.SelectNodes($"//{element}"))
                    {
                        foreach (XmlNode child in name.ChildNodes)
                        {
                            value = value.Replace(child.Name, child.Name.ToLower());
                        }
                    }
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(element));
                if (!string.IsNullOrEmpty(namespacePrefix))
                    serializer = new XmlSerializer(typeof(T), new XmlRootAttribute() { ElementName = element, Namespace = namespacePrefix });

                StringReader stringReader = new StringReader(value);
                return (T)serializer.Deserialize(stringReader);
            }
            return (T)(object)value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error parsing response", ex);

            if (typeof(T).Name.Contains("String"))
            {
                return (T)(object)"";
            }
            return (T)(object)null;
        }
    }

    public async Task<SoapCallResponse> LogAddressVerification(string payload)
    {
        try
        {
            var url = _configSettings["Address_Verification:base_url"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(payload, Encoding.UTF8, "text/xml");
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new SoapCallResponse("000", result);
            }
            return new SoapCallResponse("999", result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new SoapCallResponse("999", ex.Message);
        }

    }

    public async Task<SoapCallResponse> GetAddressVerificationStatus(string payload)
    {
        try
        {
            var url = _configSettings["Address_Verification:base_url"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(payload, Encoding.UTF8, "text/xml");
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new SoapCallResponse("000", result);
            }
            return new SoapCallResponse("999", result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new SoapCallResponse("999", ex.Message);
        }
    }

    public async Task<SoapCallResponse> DownloadAddressVerificationReport(string payload)
    {
        try
        {
            var url = _configSettings["Address_Verification:base_url"];
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(payload, Encoding.UTF8, "text/xml");
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new SoapCallResponse("000", result);
            }

            return new SoapCallResponse("999", result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new SoapCallResponse("999", ex.Message);
        }

    }
}

