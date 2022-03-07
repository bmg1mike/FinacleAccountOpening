namespace StanbicIBTC.AccountOpening.Data;

public interface IFinacleRepository
{
    string GetCityCode(string city);
    string GetStateCode(string state);
}