using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public class FinacleRepository : IFinacleRepository
{
    private IDbConnection db;

    public FinacleRepository(IConfiguration config)
    {
        db = new OracleConnection(config.GetConnectionString("FinacleConnection"));
    }

    public string GetStateCode(string state)
    {
        var sql = @$"select a.VALUE from CRMUSER.categories a,crmuser.category_lang  b  where a.bank_id = 'NG' and a.CATEGORYTYPE ='STATE' and b.LOCALETEXT = :state AND a.categoryid=b.categoryid";
        return db.Query<string>(sql, new { state = state }).SingleOrDefault();
    }

    public string GetCityCode(string city)
    {
        var sql = @$"select a.VALUE from CRMUSER.categories a,crmuser.category_lang  b where a.bank_id = 'NG' and a.CATEGORYTYPE ='CITY' and b.LOCALETEXT = :city AND a.categoryid=b.categoryid";
        return db.Query<string>(sql, new { city = city }).SingleOrDefault();
    }

    public List<OccupationResult> GetOccupations()
    {
        var sql = @$"select a.value as OccupationCode, b.localetext as Occupation from CRMUSER.categories a, crmuser.category_lang b where categorytype = 'CONTACT_OCCUPATION' and a.categoryid = b.categoryid";
        return db.Query<OccupationResult>(sql).ToList();
    }
    public List<EmploymentResult> GetEmploymentStatus()
    {
        var sql = $@"select a.value as EmploymentStatusCode, b.localetext as EmploymentStatus from CRMUSER.categories a, crmuser.category_lang b where categorytype = 'EMPLOYMENT_STATUS' and a.categoryid = b.categoryid";
        return db.Query<EmploymentResult>(sql).ToList();
    }

    public CifCheck CheckCifForBvn(string bvn)
    {
        var sql = $@"select x.cust_first_name as FirstName, x.cust_middle_name as MiddleName, x.cust_last_name as LastName, x.orgkey as Cif, x.struserfield30 as Bvn from crmuser.accounts x where x.struserfield30 = :bvn";
        return db.Query<CifCheck>(sql,new { bvn = bvn }).SingleOrDefault();

    }

    //public bool UpgradeToTierThree(string bvn)
    //{
    //    var sql = $@"";
    //}

}