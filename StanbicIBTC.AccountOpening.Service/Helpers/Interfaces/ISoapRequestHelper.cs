using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Service
{
    public interface ISoapRequestHelper
    {
        Task<SoapCallResponse> FinacleCall(string soapRequest, string soapAction = "\"treat\"", string url = "", string moduleId = "", string authId = "");
        T GetXmlTagValue<T>(string xmlObject, string element, bool withTag = false, bool deserialize = false, string namespacePrefix = "", bool ignoreCase = true);
        Task<SoapCallResponse> LogAddressVerification(string payload);
        Task<SoapCallResponse> GetAddressVerificationStatus(string payload);
        Task<SoapCallResponse> DownloadAddressVerificationReport(string payload);
    }
}