namespace StanbicIBTC.AccountOpening.Core.Repositories;
public partial interface ICIFNextOfKinDetailRepository
{
    Task<List<CIFNextOfKinDetail>> GetCIFNextOfKinDetails();
    Task<CIFNextOfKinDetail>  GetCIFNextOfKinDetail(string id);
    Task<string> CreateCIFNextOfKinDetail(CIFNextOfKinDetail cIFNextOfKinDetail);
    Task<bool> UpdateCIFNextOfKinDetail(string id, CIFNextOfKinDetail cIFNextOfKinDetail);
    Task<bool> RemoveCIFNextOfKinDetail(string id);

    //Task<List<CIFNextOfKinDetail>> GetByFieldName(string fieldName) --Template
    //Task<bool> UpdateSpecificFields(string cIFNextOfKinDetailId, CIFNextOfKinDetail cIFNextOfKinDetail) --Template

}
