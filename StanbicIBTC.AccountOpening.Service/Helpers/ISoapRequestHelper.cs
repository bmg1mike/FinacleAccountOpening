using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Service
{
    public interface ISoapRequestHelper
    {
        Task<FinacleResponse> FinacleCall(string soapRequest, string soapAction = "\"treat\"", string url = "", string moduleId = "", string authId = "");
        T GetXmlTagValue<T>(string xmlObject, string element, bool withTag = false, bool deserialize = false, string namespacePrefix = "", bool ignoreCase = true);
    }
}