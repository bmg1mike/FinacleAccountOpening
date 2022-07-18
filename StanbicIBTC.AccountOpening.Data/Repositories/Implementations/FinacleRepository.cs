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

    public bool LogForSanctionScreening(SanctionScreeningRequest request)
    {
         var sql = $@"insert into coe_staff_companion_acc_opening_sanction_screening_bot_queue(DATE_CREATED,DATE_LAST_MODIFIED,CUSTOMER_BVN,
         CUSTOMER_FIRST_NAME,CUSTOMER_FULL_HOME_ADDRESS,
         CUSTOMER_LAST_NAME,HAS_BEEN_TREATED_BY_BOT,
         ACCOUNT_OPENING_REQUEST_ID,CUSTOMER_MIDDLE_NAME) 
         Values(:DateCreated,:DateLastModified,:CustomerBvn,:CustomerFirstName,:CustomerFullHomeAddress,:CustomerLastName,:HasBeenTreatedByBot,:AccountOpeningRequestId,:CustomerMiddleName)";

         var result = db.Execute(sql, request);

        if (result < 1)
        {
            return false;
        }

        return true;
    }

    public SanctionScreeningResult GetSanctionScreeningResult(string accountOpeningId)
    {
        var sql = $@"select RESULT_SCREENSHOT_PDF_BASE64 as Pdf,SANCTION_SCREENING_PASSED as IsSuccessful from coe_staff_companion_acc_opening_sanction_screening_result where ACCOUNT_OPENING_REQUEST_ID = :accountOpeningId";
        return db.Query<SanctionScreeningResult>(sql,new{ accountOpeningId = accountOpeningId}).SingleOrDefault();
    }

    public ValidateRelationshipManagerResponse ValidateRelationshipManager(string sapId)
    {
        var sql = $@"select mpc_1.user_id, mpc_2.emp_name, mpc_2.sol_id
                    from tbaadm.upr mpc_1, tbaadm.get mpc_2
                    where mpc_1.del_flg='N'
                    and mpc_2.del_flg=mpc_1.del_flg
                    and mpc_1.role_id in ('INQUIRY','INQUIRE')
                    and mpc_1.user_appl_name='24'
                    and mpc_2.entity_cre_flg='Y'
                    and mpc_1.user_id=mpc_2.emp_id
                    and mpc_1.user_id = :sapId";

        return db.Query<ValidateRelationshipManagerResponse>(sql, new { sapId = sapId }).SingleOrDefault();
    }

    public List<BranchesResponse> GetBranches()
    {
        var sql = $@"select sol_id,sol_desc Branch_Name from tbaadm.sol";
        return db.Query<BranchesResponse>(sql).ToList();
    }

}