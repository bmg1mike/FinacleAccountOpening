using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public interface IFinacleRepository
{
    string GetCityCode(string city);
    string GetStateCode(string state);
    List<OccupationResult> GetOccupations();
    List<EmploymentResult> GetEmploymentStatus();
    CifCheck CheckCifForBvn(string bvn);
}