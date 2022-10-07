using Microsoft.EntityFrameworkCore;
using StanbicIBTC.AccountOpening.Domain;

namespace StanbicIBTC.AccountOpening.Data;

public partial class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public  DbSet<RbxBpmCifCustomDatum> RbxBpmCifCustomData { get; set; }
        public  DbSet<RbxBpmCorpCifCompany> RbxBpmCorpCifCompanies { get; set; }
        public  DbSet<RbxBpmCorpCifCustomDatum> RbxBpmCorpCifCustomData { get; set; }
        public  DbSet<RbxBpmNextOfKinDetail> RbxBpmNextOfKinDetails { get; set; }
        public  DbSet<RbxTBvnLinkageLog> RbxTBvnLinkageLogs { get; set; }
        public  DbSet<RbxRetailsUpdateCustomDatum> RbxRetailsUpdateCustomData { get; set; }
        public DbSet<RbxTFinCustCreationLog> RbxTFinCustCreationLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("REDBOX")
            //    .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<RbxTFinCustCreationLog>(entity =>
            {
                entity.ToTable("RBX_T_FIN_CUST_CREATION_LOG");

                entity.Property(e => e.Id)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AccountManager)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_MANAGER");

                entity.Property(e => e.Bvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.BvnEnrollmentBank)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN_ENROLLMENT_BANK");

                entity.Property(e => e.BvnEnrollmentBranch)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN_ENROLLMENT_BRANCH");

                entity.Property(e => e.BvnLinkageFlag)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN_LINKAGE_FLAG");

                entity.Property(e => e.ChannelId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CHANNEL_ID");

                entity.Property(e => e.ComponentServerIp)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COMPONENT_SERVER_IP");

                entity.Property(e => e.CustCoreMigDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUST_CORE_MIG_DESC");

                entity.Property(e => e.CustCoreMigStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUST_CORE_MIG_STATUS");

                entity.Property(e => e.CustomerBranchId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_BRANCH_ID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.CustomerTypeCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_TYPE_CODE");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DATE_OF_BIRTH");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_ADDRESS");

                entity.Property(e => e.EmploymentStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYMENT_STATUS");

                entity.Property(e => e.Entity)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENTITY");

                entity.Property(e => e.FinErrorSrc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIN_ERROR_SRC");

                entity.Property(e => e.FinErrorType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIN_ERROR_TYPE");

                entity.Property(e => e.FinRespCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIN_RESP_CODE");

                entity.Property(e => e.FinRespDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIN_RESP_DESC");

                entity.Property(e => e.FinRespStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIN_RESP_STATUS");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.IsCustomerNonResident)
                    .HasPrecision(10)
                    .HasColumnName("IS_CUSTOMER_NON_RESIDENT");

                entity.Property(e => e.IsStaffFlag)
                    .HasPrecision(10)
                    .HasColumnName("IS_STAFF_FLAG");

                entity.Property(e => e.LanguageCodes)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LANGUAGE_CODES");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.LifeTimeId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LIFE_TIME_ID");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MARITAL_STATUS");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NATIONALITY");

                entity.Property(e => e.OccupationCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OCCUPATION_CODE");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.PhysicalAddressDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHYSICAL_ADDRESS_DETAILS");

                entity.Property(e => e.ProdReqInTime)
                    .HasPrecision(6)
                    .HasColumnName("PROD_REQ_IN_TIME");

                entity.Property(e => e.ProdReqOutTime)
                    .HasPrecision(6)
                    .HasColumnName("PROD_REQ_OUT_TIME");

                entity.Property(e => e.ProdRespInTime)
                    .HasPrecision(6)
                    .HasColumnName("PROD_RESP_IN_TIME");

                entity.Property(e => e.ProdRespOutTime)
                    .HasPrecision(6)
                    .HasColumnName("PROD_RESP_OUT_TIME");

                entity.Property(e => e.RelationshipOpeningDate)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP_OPENING_DATE");

                entity.Property(e => e.RequestUuid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REQUEST_UUID");

                entity.Property(e => e.RespCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RESP_CODE");

                entity.Property(e => e.RespDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RESP_DESC");

                entity.Property(e => e.RespStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RESP_STATUS");

                entity.Property(e => e.Salutation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SALUTATION");

                entity.Property(e => e.SegmentationClass)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SEGMENTATION_CLASS");

                entity.Property(e => e.ServiceRequestId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE_REQUEST_ID");

                entity.Property(e => e.SrcModuleId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SRC_MODULE_ID");

                entity.Property(e => e.SrcTranRef)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SRC_TRAN_REF");

                entity.Property(e => e.StaffEmployeeId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STAFF_EMPLOYEE_ID");

                entity.Property(e => e.SubSegment)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SUB_SEGMENT");

                entity.Property(e => e.SupportingDocumentData)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SUPPORTING_DOCUMENT_DATA");

                entity.Property(e => e.TaxDeductionTable)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TAX_DEDUCTION_TABLE");

                entity.Property(e => e.TranDate)
                    .HasColumnType("DATE")
                    .HasColumnName("TRAN_DATE");

                entity.Property(e => e.TranTime)
                    .HasPrecision(6)
                    .HasColumnName("TRAN_TIME");
            });

            modelBuilder.Entity<RbxBpmCifCustomDatum>(entity =>
            {
                entity.ToTable("RBX_BPM_CIF_CUSTOM_DATA");

                entity.Property(e => e.Id)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AccomodationType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ACCOMODATION_TYPE");

                entity.Property(e => e.AssetClassification)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ASSET_CLASSIFICATION");

                entity.Property(e => e.Base2Indicator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BASE2_INDICATOR");

                entity.Property(e => e.BaselIiIndicator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BASEL_II_INDICATOR");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.Bvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.CounterPartyInfo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTER_PARTY_INFO");

                entity.Property(e => e.CounterPartyInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTER_PARTY_INFORMATION");

                entity.Property(e => e.CountryOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_OF_BIRTH");

                entity.Property(e => e.CountryOfTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_OF_TAX_RESIDENCE");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.DateCreated)
                    .HasPrecision(6)
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.DefaultAddressType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DEFAULT_ADDRESS_TYPE");

                entity.Property(e => e.DistributionChannel)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DISTRIBUTION_CHANNEL");

                entity.Property(e => e.EmploymentType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYMENT_TYPE");

                entity.Property(e => e.FnrClassification)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FNR_CLASSIFICATION");

                entity.Property(e => e.FnrStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FNR_STATUS");

                entity.Property(e => e.ForeignCustomer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FOREIGN_CUSTOMER");

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDENTITY_NUMBER");

                entity.Property(e => e.IdentityType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDENTITY_TYPE");

                entity.Property(e => e.IndustryClassificationCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INDUSTRY_CLASSIFICATION_CODE");

                entity.Property(e => e.IndustrySarbCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INDUSTRY_SARB_CODE");

                entity.Property(e => e.InsiderToBank)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSIDER_TO_BANK");

                entity.Property(e => e.IsCoreProfileActive)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_CORE_PROFILE_ACTIVE");

                entity.Property(e => e.IsCustomerMinor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_CUSTOMER_MINOR");

                entity.Property(e => e.IsOnlyCountryOfNationality)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_ONLY_COUNTRY_OF_NATIONALITY");

                entity.Property(e => e.IsOnlyCountryTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_ONLY_COUNTRY_TAX_RESIDENCE");

                entity.Property(e => e.IsOnlyNationality)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_ONLY_NATIONALITY");

                entity.Property(e => e.KeyContactPerson)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("KEY_CONTACT_PERSON");

                entity.Property(e => e.KeyContactPersonName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("KEY_CONTACT_PERSON_NAME");

                entity.Property(e => e.KycIndicator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("KYC_INDICATOR");

                entity.Property(e => e.LegalChallengeStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LEGAL_CHALLENGE_STATUS");

                entity.Property(e => e.LegalEntity)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LEGAL_ENTITY");

                entity.Property(e => e.LocalIndicia)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LOCAL_INDICIA");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MARITAL_STATUS");

                entity.Property(e => e.MonthlyNetIncome)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MONTHLY_NET_INCOME");

                entity.Property(e => e.NationalIdNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NATIONAL_ID_NUMBER");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OCCUPATION");

                entity.Property(e => e.OnlyCountryOfTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ONLY_COUNTRY_OF_TAX_RESIDENCE");

                entity.Property(e => e.PoliticallyExposed)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("POLITICALLY_EXPOSED");

                entity.Property(e => e.PortfolioNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PORTFOLIO_NUMBER");

                entity.Property(e => e.PreferredName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PREFERRED_NAME");

                entity.Property(e => e.PriCountryOfTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRI_COUNTRY_OF_TAX_RESIDENCE");

                entity.Property(e => e.PrimaryNationality)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_NATIONALITY");

                entity.Property(e => e.PrimarySicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_SIC_CODE");

                entity.Property(e => e.PrimaryTaxResidenceCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_TAX_RESIDENCE_COUNTRY");

                entity.Property(e => e.Region)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REGION");

                entity.Property(e => e.Relationship)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP");

                entity.Property(e => e.ReserveBankCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RESERVE_BANK_CODE");

                entity.Property(e => e.ReturnsClassCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RETURNS_CLASS_CODE");

                entity.Property(e => e.ReturnsClassificationCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RETURNS_CLASSIFICATION_CODE");

                entity.Property(e => e.RsvBankCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RSV_BANK_CODE");

                entity.Property(e => e.SecondaryRmId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SECONDARY_RM_ID");

                entity.Property(e => e.SecondarySicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SECONDARY_SIC_CODE");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SHORT_NAME");

                entity.Property(e => e.TaxIdNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TAX_ID_NUMBER");

                entity.Property(e => e.TaxIdentificationNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TAX_IDENTIFICATION_NUMBER");

                entity.Property(e => e.TertiarySicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TERTIARY_SIC_CODE");

                entity.Property(e => e.ValidateFnr)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VALIDATE_FNR");

                entity.Property(e => e.WithHoldingTax)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WITH_HOLDING_TAX");

                entity.Property(e => e.WithholdingTax1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WITHHOLDING_TAX");

                entity.Property(e => e.Zip)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ZIP");
            });

            modelBuilder.Entity<RbxBpmCorpCifCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("SYS_C0016340");

                entity.ToTable("RBX_BPM_CORP_CIF_COMPANY");

                entity.Property(e => e.CompanyId)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.BusinessAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_ADDRESS");

                entity.Property(e => e.CountryOfIncorporation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_OF_INCORPORATION");

                entity.Property(e => e.CountryOfRegistration)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_OF_REGISTRATION");

                entity.Property(e => e.DateCreated)
                    .HasPrecision(6)
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.FkCustomData)
                    .HasPrecision(19)
                    .HasColumnName("FK_CUSTOM_DATA");

                entity.Property(e => e.HeadOfficeAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HEAD_OFFICE_ADDRESS");

                entity.Property(e => e.IsForeignCompany)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IS_FOREIGN_COMPANY");

                entity.Property(e => e.PrincipalOfficeCountry)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRINCIPAL_OFFICE_COUNTRY");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REGISTERED_ADDRESS");

                entity.Property(e => e.RegisteredName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REGISTERED_NAME");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REGISTRATION_NUMBER");

                entity.Property(e => e.TradeName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRADE_NAME");

                entity.HasOne(d => d.FkCustomDataNavigation)
                    .WithMany(p => p.RbxBpmCorpCifCompanies)
                    .HasForeignKey(d => d.FkCustomData)
                    .HasConstraintName("FK_6CATLOTUA2JAI97SILH40P18S");
            });

            modelBuilder.Entity<RbxBpmCorpCifCustomDatum>(entity =>
            {
                entity.ToTable("RBX_BPM_CORP_CIF_CUSTOM_DATA");

                entity.Property(e => e.Id)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BusinessType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_TYPE");

                entity.Property(e => e.Bvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.CorporateType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CORPORATE_TYPE");

                entity.Property(e => e.CounterPartyInfo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTER_PARTY_INFO");

                entity.Property(e => e.CountryOfTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_OF_TAX_RESIDENCE");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.CustomerType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_TYPE");

                entity.Property(e => e.DateCreated)
                    .HasPrecision(6)
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.DistributionChannel)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DISTRIBUTION_CHANNEL");

                entity.Property(e => e.Earns50PercentGross)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EARNS_50_PERCENT_GROSS");

                entity.Property(e => e.EntityClass)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENTITY_CLASS");

                entity.Property(e => e.FatCaExempt)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FAT_CA_EXEMPT");

                entity.Property(e => e.FinancialInstitution)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FINANCIAL_INSTITUTION");

                entity.Property(e => e.FnrClassification)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FNR_CLASSIFICATION");

                entity.Property(e => e.FnrStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FNR_STATUS");

                entity.Property(e => e.FundSource)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FUND_SOURCE");

                entity.Property(e => e.Holds50PercentGross)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HOLDS_50_PERCENT_GROSS");

                entity.Property(e => e.IdIssuedOrganization)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ID_ISSUED_ORGANIZATION");

                entity.Property(e => e.IncomeTaxNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INCOME_TAX_NUMBER");

                entity.Property(e => e.IndustryClassificationCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INDUSTRY_CLASSIFICATION_CODE");

                entity.Property(e => e.InsiderToBank)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSIDER_TO_BANK");

                entity.Property(e => e.KeyContactPerson)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("KEY_CONTACT_PERSON");

                entity.Property(e => e.KycIndicator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("KYC_INDICATOR");

                entity.Property(e => e.LegalChallengeStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LEGAL_CHALLENGE_STATUS");

                entity.Property(e => e.LegalEntityType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LEGAL_ENTITY_TYPE");

                entity.Property(e => e.ListApprovedExchanges)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LIST_APPROVED_EXCHANGES");

                entity.Property(e => e.MonthlyNetIncome)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MONTHLY_NET_INCOME");

                entity.Property(e => e.OnlyCountryOfTaxResidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ONLY_COUNTRY_OF_TAX_RESIDENCE");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.PoliticallyExposed)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("POLITICALLY_EXPOSED");

                entity.Property(e => e.PortfolioNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PORTFOLIO_NUMBER");

                entity.Property(e => e.PreferredContactType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PREFERRED_CONTACT_TYPE");

                entity.Property(e => e.PrimaryEmailType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_EMAIL_TYPE");

                entity.Property(e => e.PrimarySicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PRIMARY_SIC_CODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REGION");

                entity.Property(e => e.Relationship)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP");

                entity.Property(e => e.ReturnsClassCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RETURNS_CLASS_CODE");

                entity.Property(e => e.RsvBankCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RSV_BANK_CODE");

                entity.Property(e => e.SbgMember)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SBG_MEMBER");

                entity.Property(e => e.ScumRegNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCUM_REG_NUMBER");

                entity.Property(e => e.SecSicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SEC_SIC_CODE");

                entity.Property(e => e.SecondaryRmId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SECONDARY_RM_ID");

                entity.Property(e => e.StateOfInc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STATE_OF_INC");

                entity.Property(e => e.TertiarySicCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TERTIARY_SIC_CODE");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("UNIQUE_ID");

                entity.Property(e => e.UsRelatedParties)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("US_RELATED_PARTIES");

                entity.Property(e => e.W8BenReceived)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("W8_BEN_RECEIVED");

                entity.Property(e => e.Website)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WEBSITE");

                entity.Property(e => e.WithholdingTax)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WITHHOLDING_TAX");
            });

            modelBuilder.Entity<RbxBpmNextOfKinDetail>(entity =>
            {
                entity.ToTable("RBX_BPM_NEXT_OF_KIN_DETAILS");

                entity.Property(e => e.Id)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.AddressFormat)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_FORMAT");

                entity.Property(e => e.DateCreated)
                    .HasPrecision(6)
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_OF_BIRTH");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_ADDRESS");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.FkCustomerAddDetails)
                    .HasPrecision(19)
                    .HasColumnName("FK_CUSTOMER_ADD_DETAILS");

                entity.Property(e => e.Gender)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.RelationshipType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP_TYPE");

                entity.HasOne(d => d.FkCustomerAddDetailsNavigation)
                    .WithMany(p => p.RbxBpmNextOfKinDetails)
                    .HasForeignKey(d => d.FkCustomerAddDetails)
                    .HasConstraintName("FK_A0THOFDBVKI693PXJCNJNJFTI");
            });

            modelBuilder.Entity<RbxTBvnLinkageLog>(entity =>
            {
                entity.ToTable("RBX_T_BVN_LINKAGE_LOG");

                entity.Property(e => e.Id)
                    .HasPrecision(19)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AcctName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ACCT_NAME");

                entity.Property(e => e.AcctNo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ACCT_NO");

                entity.Property(e => e.BankEnrolled)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BANK_ENROLLED");

                entity.Property(e => e.BranchEnrolled)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH_ENROLLED");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH_NAME");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH_NO");

                entity.Property(e => e.BvnForm)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN_FORM");

                entity.Property(e => e.BvnNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BVN_NUMBER");

                entity.Property(e => e.CifId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CIF_ID");

                entity.Property(e => e.ComponentServerIp)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COMPONENT_SERVER_IP");

                entity.Property(e => e.CreateDate)
                    .HasPrecision(6)
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NO");

                entity.Property(e => e.RecordDelFlag)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RECORD_DEL_FLAG");

                entity.Property(e => e.RecordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RECORD_HASH");

                entity.Property(e => e.RecordIsFinalFlag)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RECORD_IS_FINAL_FLAG");

                entity.Property(e => e.RecordSubmissionFlag)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RECORD_SUBMISSION_FLAG");

                entity.Property(e => e.Sapid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SAPID");

                entity.Property(e => e.TranDate)
                    .HasColumnType("DATE")
                    .HasColumnName("TRAN_DATE");

                entity.Property(e => e.TranTime)
                    .HasPrecision(6)
                    .HasColumnName("TRAN_TIME");

                entity.Property(e => e.TransactionStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRANSACTION_STATUS");

                entity.Property(e => e.WkfId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WKF_ID");
            });

        modelBuilder.Entity<RbxRetailsUpdateCustomDatum>(entity =>
        {
            entity.HasKey(e => e.CustomerId)
                .HasName("RBX_RETAILS_UPDATE_CUSTOM_DATA_PK");

            entity.ToTable("RBX_RETAILS_UPDATE_CUSTOM_DATA");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_ID");

            entity.Property(e => e.AssetClassification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ASSET_CLASSIFICATION");

            entity.Property(e => e.BranchId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ID");

            entity.Property(e => e.CounterPartyInformation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("COUNTER_PARTY_INFORMATION");

            entity.Property(e => e.CountryOfTaxResidence)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_OF_TAX_RESIDENCE");

            entity.Property(e => e.DefaultAddressType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DEFAULT_ADDRESS_TYPE");

            entity.Property(e => e.DistributionChannel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DISTRIBUTION_CHANNEL");

            entity.Property(e => e.EmploymentType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMPLOYMENT_TYPE");

            entity.Property(e => e.FnrClassification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FNR_CLASSIFICATION");

            entity.Property(e => e.FnrStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FNR_STATUS");

            entity.Property(e => e.ForeignCustomer)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FOREIGN_CUSTOMER");

            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IDENTITY_NUMBER");

            entity.Property(e => e.IndustryClassificationCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("INDUSTRY_CLASSIFICATION_CODE");

            entity.Property(e => e.IsCoreProfileActive)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IS_CORE_PROFILE_ACTIVE");

            entity.Property(e => e.IsCustomerMinor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IS_CUSTOMER_MINOR");

            entity.Property(e => e.IsOnlyCountryOfNationality)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IS_ONLY_COUNTRY_OF_NATIONALITY");

            entity.Property(e => e.IsOnlyCountryTaxResidence)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IS_ONLY_COUNTRY_TAX_RESIDENCE");

            entity.Property(e => e.KeyContactPersonName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("KEY_CONTACT_PERSON_NAME");

            entity.Property(e => e.KycIndicator)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("KYC_INDICATOR");

            entity.Property(e => e.LegalChallengeStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LEGAL_CHALLENGE_STATUS");

            entity.Property(e => e.LegalEntity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LEGAL_ENTITY");

            entity.Property(e => e.LocalIndicia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCAL_INDICIA");

            entity.Property(e => e.MonthlyNetIncome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MONTHLY_NET_INCOME");

            entity.Property(e => e.NokAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS");

            entity.Property(e => e.NokAddressCategory)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS_CATEGORY");

            entity.Property(e => e.NokAddressFormat)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS_FORMAT");

            entity.Property(e => e.NokAddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS_LINE1");

            entity.Property(e => e.NokAddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS_LINE2");

            entity.Property(e => e.NokAddressLine3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_ADDRESS_LINE3");

            entity.Property(e => e.NokCity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_CITY");

            entity.Property(e => e.NokCountry)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_COUNTRY");

            entity.Property(e => e.NokDateOfBirth)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_DATE_OF_BIRTH");

            entity.Property(e => e.NokEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_EMAIL");

            entity.Property(e => e.NokFirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_FIRST_NAME");

            entity.Property(e => e.NokGender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_GENDER");

            entity.Property(e => e.NokLastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_LAST_NAME");

            entity.Property(e => e.NokLocalityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_LOCALITY_NAME");

            entity.Property(e => e.NokMiddleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_MIDDLE_NAME");

            entity.Property(e => e.NokPhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_PHONE_NUMBER");

            entity.Property(e => e.NokPostalCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_POSTAL_CODE");

            entity.Property(e => e.NokPreferredAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_PREFERRED_ADDRESS");

            entity.Property(e => e.NokPreferredFormat)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_PREFERRED_FORMAT");

            entity.Property(e => e.NokRelationshipType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_RELATIONSHIP_TYPE");

            entity.Property(e => e.NokStateCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_STATE_CODE");

            entity.Property(e => e.NokStreetName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOK_STREET_NAME");

            entity.Property(e => e.PoliticallyExposed)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("POLITICALLY_EXPOSED");

            entity.Property(e => e.PrimaryNationality)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PRIMARY_NATIONALITY");

            entity.Property(e => e.PrimaryTaxResidenceCountry)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PRIMARY_TAX_RESIDENCE_COUNTRY");

            entity.Property(e => e.Relationship)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RELATIONSHIP");

            entity.Property(e => e.ReserveBankCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RESERVE_BANK_CODE");

            entity.Property(e => e.ReturnsClassificationCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RETURNS_CLASSIFICATION_CODE");

            entity.Property(e => e.TaxIdentificationNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TAX_IDENTIFICATION_NUMBER");

            entity.Property(e => e.ValidateFnr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("VALIDATE_FNR");

            entity.Property(e => e.WithholdingTax)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("WITHHOLDING_TAX");

            entity.Property(e => e.Zip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ZIP");
        });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

