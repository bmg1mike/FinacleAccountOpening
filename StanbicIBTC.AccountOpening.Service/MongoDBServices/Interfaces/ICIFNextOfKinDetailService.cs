namespace StanbicIBTC.AccountOpening.Core.Services;

public partial interface ICIFNextOfKinDetailService
{
    Task<List<CIFNextOfKinDetail>> GetCIFNextOfKinDetails();
    Task<CIFNextOfKinDetail>  GetCIFNextOfKinDetail(string id);
    Task<string> CreateCIFNextOfKinDetail(CIFNextOfKinDetail cIFNextOfKinDetail);
    Task<bool> UpdateCIFNextOfKinDetail(string id, CIFNextOfKinDetail cIFNextOfKinDetail);
    Task<bool> RemoveCIFNextOfKinDetail(string id);
}
