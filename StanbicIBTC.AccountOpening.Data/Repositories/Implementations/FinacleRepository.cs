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
        var sql = @$"
                    select a.VALUE 
                    from CRMUSER.categories a,crmuser.category_lang  b 
                    where a.bank_id = 'NG' and a.CATEGORYTYPE ='STATE' and b.LOCALETEXT = '@state' AND a.categoryid=b.categoryid;
                    ";
        return db.Query<string>(sql, new { @state = state }).Single();
    }

    public string GetCityCode(string city)
    {
        var sql = @$"
                    select a.VALUE 
                    from CRMUSER.categories a,crmuser.category_lang  b 
                    where a.bank_id = 'NG' and a.CATEGORYTYPE ='CITY' and b.LOCALETEXT = '@city' AND a.categoryid=b.categoryid;
                    ";
        return db.Query<string>(sql, new { @city = city }).Single();
    }
}