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

    public FinacleAccountDetailResponse GetAccountDetailsByAccountNumber(string accountNumber)
    {
        var sql = $"select cif_id as Cif,schm_code as SchemeCode,acct_name as AccountName,GL_SUB_HEAD_CODE as GlSubHeadCode from tbaadm.gam where foracid = :accountNumber";
        return db.Query<FinacleAccountDetailResponse>(sql, new { accountNumber = accountNumber }).SingleOrDefault();
    }

    public FinacleAccountDetailResponse GetAccountDetailsByCif(string cif)
    {
        var sql = $"select cif_id as Cif,schm_code as SchemeCode,acct_name as AccountName from tbaadm.gam where cif_id = :cif";
        return db.Query<FinacleAccountDetailResponse>(sql, new { cif = cif }).SingleOrDefault();
    }

    public bool UpgradeToTierThree(string accountNumber)
    {
        var sql = $@"update tbaadm.gam set schm_code = 'SB001' where foracid = :accountNumber";
        var result = db.Execute(sql, new {accountNumber = accountNumber});
        if (result < 1)
        {
            return false;
        }

        return true;
    }

}