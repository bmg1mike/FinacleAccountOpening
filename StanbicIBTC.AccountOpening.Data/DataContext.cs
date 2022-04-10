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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("REDBOX")
                .UseCollation("USING_NLS_COMP");

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

        modelBuilder.HasSequence("ACC_MAINTENANCE_SEQ");

            modelBuilder.HasSequence("AIRTIME_TSQ_SEQ");

            modelBuilder.HasSequence("BOT_PROCESSING_SEQ");

            modelBuilder.HasSequence("BOT_REMEDY_SEQ");

            modelBuilder.HasSequence("BOT_S_ISWCIPG_AUDIT_TRAIL_SEQ");

            modelBuilder.HasSequence("BOT_S_ISWCIPG_POSTING_FILE_SEQ");

            modelBuilder.HasSequence("BOT_S_ISWCIPG_TERMINAL_ACC_SEQ");

            modelBuilder.HasSequence("BOT_S_MERCHANT_FEE_TYPE_SEQ");

            modelBuilder.HasSequence("BOT_T464_SEQ");

            modelBuilder.HasSequence("BULK_NIP_NAME_ENQ_DETAILS_SEQ");

            modelBuilder.HasSequence("BULK_NIP_NAME_ENQ_TRANS_SEQ");

            modelBuilder.HasSequence("BULK_NIP_NAME_ENQ_TRANS_SEQ1");

            modelBuilder.HasSequence("BULK_NIP_NAME_ENQ_TRANS_SEQ2");

            modelBuilder.HasSequence("CARP_BATCH_GROUP_MASTER_SEQ");

            modelBuilder.HasSequence("CARP_CCY_DISTRBTN_SEQ");

            modelBuilder.HasSequence("CARP_CCY_MGT_ASSET_SEQ");

            modelBuilder.HasSequence("CARP_CCY_MGT_EXPNDTR_SEQ");

            modelBuilder.HasSequence("CARP_CCY_STORAGE_SEQ");

            modelBuilder.HasSequence("CARP_CURR_DISTRBTN_SEQ");

            modelBuilder.HasSequence("CARP_CURR_MGT_ASSET_SEQ");

            modelBuilder.HasSequence("CARP_CURR_MGT_EXPNDTR_SEQ");

            modelBuilder.HasSequence("CARP_CURR_STORAGE_SEQ");

            modelBuilder.HasSequence("CARP_CURR_STORAGES_SEQ");

            modelBuilder.HasSequence("CARP_CURRY_DISTRBTN_SEQ");

            modelBuilder.HasSequence("CARP_CURRY_MGT_ASSET_SEQ");

            modelBuilder.HasSequence("CARP_CURRY_MGT_EXPNDTR_SEQ");

            modelBuilder.HasSequence("CARP_CURRY_STORAGE_SEQ");

            modelBuilder.HasSequence("CARP_OPERATION_LOG_SEQ");

            modelBuilder.HasSequence("DBOBJECTID_SEQUENCE").IncrementsBy(50);

            modelBuilder.HasSequence("EBILLS_SEQ");

            modelBuilder.HasSequence("FX_ACCT_CRTN_LOG_SEQ");

            modelBuilder.HasSequence("GETCAPSA_SEQ");

            modelBuilder.HasSequence("HIBERNATE_SEQUENCE");

            modelBuilder.HasSequence("ID_SEQ");

            modelBuilder.HasSequence("phone_val_seq");

            modelBuilder.HasSequence("QUICKTELLER_TRAN_ID_SEQ");

            modelBuilder.HasSequence("RBX_AIRTEL_SMARTRECHARGE_SEQ");

            modelBuilder.HasSequence("RBX_AIRTELRECHARGE_SEQ");

            modelBuilder.HasSequence("RBX_AIRTIME_SEQ");

            modelBuilder.HasSequence("RBX_AIRTIME_TOPUP_SEQ");

            modelBuilder.HasSequence("RBX_AIRTIMEAGGR_SEQ");

            modelBuilder.HasSequence("RBX_ARBITER_SEQUENCE");

            modelBuilder.HasSequence("RBX_ATM_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_AUTHTOKEN_SEQ");

            modelBuilder.HasSequence("RBX_AUTHTOKENHIST_SEQ");

            modelBuilder.HasSequence("RBX_AUTO_TOPUP_SUBSCRIBERS_SEQ");

            modelBuilder.HasSequence("RBX_BILLERS_SEQ");

            modelBuilder.HasSequence("RBX_BOL_NIP_BATCH_INFO_SEQ");

            modelBuilder.HasSequence("RBX_BULK_AIRTIME_BATCH_SEQ");

            modelBuilder.HasSequence("RBX_BULK_AIRTIME_ITEM_SEQ");

            modelBuilder.HasSequence("RBX_BVN_COLLECTOR_EXCP_SEQ");

            modelBuilder.HasSequence("RBX_BVN_COLLECTOR_SEQ");

            modelBuilder.HasSequence("RBX_CALYPSO_BOT_SEQ");

            modelBuilder.HasSequence("RBX_CAPS_POSTCARD_SEQ");

            modelBuilder.HasSequence("RBX_CAPS_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_CAPS_SMARTCARD_PROC_SEQ");

            modelBuilder.HasSequence("RBX_CARP_CCY_DISTRBTN_SEQ");

            modelBuilder.HasSequence("RBX_CARP_SORT_CODE_SEQ");

            modelBuilder.HasSequence("RBX_CARP_SORT_CODES_SEQ");

            modelBuilder.HasSequence("RBX_CBA_WEB_SEQ");

            modelBuilder.HasSequence("RBX_CBN_CRMS_P_SEQ");

            modelBuilder.HasSequence("RBX_CBN_INFO_REQ_LOG_SEQ");

            modelBuilder.HasSequence("RBX_CHANNEL_LOG_SEQ");

            modelBuilder.HasSequence("RBX_CHARGE_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_CHARGE_SEQ");

            modelBuilder.HasSequence("RBX_CHARGE_TYPE_SEQ");

            modelBuilder.HasSequence("RBX_CHEQUEUTILITY_SEQ");

            modelBuilder.HasSequence("RBX_CIPG_PROCESSING_FILE_SEQ");

            modelBuilder.HasSequence("RBX_CIPG_WEB_SEQ");

            modelBuilder.HasSequence("RBX_CLICKATELL_SECRET_SEQ");

            modelBuilder.HasSequence("RBX_CLICKATELL_SEQ");

            modelBuilder.HasSequence("RBX_CMMS_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_CORALPAY_P_SEQ");

            modelBuilder.HasSequence("RBX_CORALPAY_SEQUENCE");

            modelBuilder.HasSequence("RBX_CORALPAY_VAS_PAYMENT_SEQ");

            modelBuilder.HasSequence("RBX_CORALPAY_VAS_SEQ");

            modelBuilder.HasSequence("RBX_COV_UNI_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_CRMS_T_SEQ");

            modelBuilder.HasSequence("RBX_CROSSCURRENCY_SEQ");

            modelBuilder.HasSequence("RBX_D_CHANNEL_MONITOR_SEQ");

            modelBuilder.HasSequence("RBX_D_CHANNEL_OPERATION_STATUS_SEQ");

            modelBuilder.HasSequence("RBX_DD_BILLER_INFO_SEQ");

            modelBuilder.HasSequence("RBX_DD_MANDATE_INFO_SEQ");

            modelBuilder.HasSequence("RBX_DD_PAYMENT_INFO_SEQ");

            modelBuilder.HasSequence("RBX_DD_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_DEVICE_BND_SEQ");

            modelBuilder.HasSequence("RBX_DO_FUNDS_TRANSFER_LOG_SEQ");

            modelBuilder.HasSequence("RBX_DSTV_TRAN_ID_SEQ");

            modelBuilder.HasSequence("RBX_EBILLS_LIEN_CFG_SEQ");

            modelBuilder.HasSequence("RBX_EBILLS_TRANS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_ETRANZACT_SEQ");

            modelBuilder.HasSequence("RBX_EXCHANGE_ACCT_INFO_CFG_SEQ");

            modelBuilder.HasSequence("RBX_FIN_ACCT_OPENING_SEQ");

            modelBuilder.HasSequence("RBX_FIN_BETA_SEQ");

            modelBuilder.HasSequence("RBX_FIN_BVN_LINKAGE_SEQ");

            modelBuilder.HasSequence("RBX_FIN_CHARGE_COLL_SEQ");

            modelBuilder.HasSequence("RBX_FIN_CUST_CREATION_SEQ");

            modelBuilder.HasSequence("RBX_FIN_RTGS_NEFT_SEQ");

            modelBuilder.HasSequence("RBX_FIN_SANC_LMT_MOD_SEQ");

            modelBuilder.HasSequence("RBX_FIN_TRANSFER_SEQ");

            modelBuilder.HasSequence("RBX_FIN_TRF_UUID_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_FINACLE_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_ACCT_INFO_CFG_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_FEE_VAT_INFO_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_SECURITY_CFG_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_SETTLEMENT_INFO_SEQ");

            modelBuilder.HasSequence("RBX_FMDQ_WITHDRAWAL_INFO_SEQ");

            modelBuilder.HasSequence("RBX_FX_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_FXPURCHASE_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_FYNEFIELD_VALIDATION_SEQ");

            modelBuilder.HasSequence("RBX_HIBERNATE_SEQ");

            modelBuilder.HasSequence("RBX_ICAD_OPENED_ACCT_SEQ");

            modelBuilder.HasSequence("RBX_ICAD_PUSH_INFO_SEQ");

            modelBuilder.HasSequence("RBX_IDENTITY_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_IMAGE_VERIFICATION_LOG_SEQ");

            modelBuilder.HasSequence("RBX_INFOWARE_SEQ");

            modelBuilder.HasSequence("RBX_INSURANCE_REQ_DTD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_INSURANCE_USER_SEQ");

            modelBuilder.HasSequence("RBX_INTERBANK_TRF_SEQ");

            modelBuilder.HasSequence("RBX_ISO_TRF_LOG_SEQ");

            modelBuilder.HasSequence("RBX_ISO_TRF_TRXN_REF_SEQ");

            modelBuilder.HasSequence("RBX_LIFE_TIME_ID_SEQ");

            modelBuilder.HasSequence("RBX_MASTER_CARD_PMT_LOG_SEQ");

            modelBuilder.HasSequence("RBX_MAUTECH_SEQ");

            modelBuilder.HasSequence("RBX_MBANKINTERSWITCHINTGR_SEQ");

            modelBuilder.HasSequence("RBX_MERCHANT_PAY_SEQ");

            modelBuilder.HasSequence("RBX_MERCHANT_TXN_SEQ");

            modelBuilder.HasSequence("RBX_MERCHANT_USSD_SEQ");

            modelBuilder.HasSequence("RBX_MFB_SEQ");

            modelBuilder.HasSequence("RBX_MOBILEMONEY_SEQ");

            modelBuilder.HasSequence("RBX_MTN_AR_ACCT_NUM_CACHE_SEQ");

            modelBuilder.HasSequence("RBX_MTN_AUTOMATIC_RECHARGE_SEQ");

            modelBuilder.HasSequence("RBX_MTN_DIRECT_SEQ");

            modelBuilder.HasSequence("RBX_MTN_PRODUCT_CATALOGUE_SEQ");

            modelBuilder.HasSequence("RBX_MTN_RECHARGE_SEQ");

            modelBuilder.HasSequence("RBX_MYBANK_RECON_SEQ");

            modelBuilder.HasSequence("RBX_NACS_ACCT_INFO_ENQ_SEQ");

            modelBuilder.HasSequence("RBX_NACS_BATCH_INFO_LOG_SEQ");

            modelBuilder.HasSequence("RBX_NAPSIN_REMITA_ACCT_LOG_SEQ");

            modelBuilder.HasSequence("RBX_NAPSINBOUND_SEQ");

            modelBuilder.HasSequence("RBX_NAPSOUT_SEQ");

            modelBuilder.HasSequence("RBX_NAPSOUT_TRAN_ITEM_LOG_SEQ");

            modelBuilder.HasSequence("RBX_NCS_ASSESSMENT_FILE_SEQ");

            modelBuilder.HasSequence("RBX_NIBBS_BVN_SEQ");

            modelBuilder.HasSequence("RBX_NIBSSPAY_PAYMENT_SEQ");

            modelBuilder.HasSequence("RBX_NIP_DTD_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_NIP_LIMIT_SEQ");

            modelBuilder.HasSequence("RBX_NIP_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_NIPINBOUND_SEQ");

            modelBuilder.HasSequence("RBX_NIPOUT_LIMIT_EXCEPTION_SEQ");

            modelBuilder.HasSequence("RBX_NIPOUT_SEQ");

            modelBuilder.HasSequence("RBX_OFI_ACCT_INFO_CFG_SEQ");

            modelBuilder.HasSequence("RBX_OFI_PROFILE_INFO_CFG_SEQ");

            modelBuilder.HasSequence("RBX_OFI_REQ_DTD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_OFI_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_OFI_TRF_DTD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_P_AIRTIMEAGGR_SEQ");

            modelBuilder.HasSequence("RBX_P_CHANNEL_LOG_SEQ");

            modelBuilder.HasSequence("RBX_P_CHANNEL_SEQ");

            modelBuilder.HasSequence("RBX_P_GSI_ACCT_STATUS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_P_PAYCODE_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_P_TSA_REVENUE_COLL_SEQ");

            modelBuilder.HasSequence("RBX_PAYCODE_REQUEST_SEQ");

            modelBuilder.HasSequence("RBX_PERMISSIONS_SEQ");

            modelBuilder.HasSequence("RBX_PHONE_NUM_VAL_SEQ");

            modelBuilder.HasSequence("RBX_PHONE_VAL_SEQ");

            modelBuilder.HasSequence("RBX_PHONE_VALIDATION_SEQ");

            modelBuilder.HasSequence("RBX_PREM_BET_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_PREM_LOTTO_ACCT_SEQ");

            modelBuilder.HasSequence("RBX_PREM_LOTTO_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_PREM_LOTTO_TXN_SEQ");

            modelBuilder.HasSequence("RBX_QR_MSISDN_DTD_SEQ");

            modelBuilder.HasSequence("RBX_QUICKR_AUDITLOG_SEQ");

            modelBuilder.HasSequence("RBX_QUICKR_EXCEPTION_SEQ");

            modelBuilder.HasSequence("RBX_QUICKR_EXPN_TEMP_SEQ");

            modelBuilder.HasSequence("RBX_QUICKR_NEW_EXCEPTION_SEQ");

            modelBuilder.HasSequence("RBX_QUICKR_TEMP_EXCEPTION_SEQ");

            modelBuilder.HasSequence("RBX_QUICKRECHARGE_SEQ");

            modelBuilder.HasSequence("RBX_QUICKRECHARGE_TRXNID_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_QUICKTELLER_SEQ");

            modelBuilder.HasSequence("RBX_QUICKTELLER_SETTLEMENT_SEQ");

            modelBuilder.HasSequence("RBX_QUICKTELLERFUNDWALLET_SEQ");

            modelBuilder.HasSequence("RBX_RAINOIL_VALIDATION_SEQ");

            modelBuilder.HasSequence("RBX_REMITA_SEQ");

            modelBuilder.HasSequence("RBX_REMITA_SEQ01");

            modelBuilder.HasSequence("RBX_REMITAGOVCOLLECT_SEQ");

            modelBuilder.HasSequence("RBX_RETAIL_CUST_UPDATE_SEQ");

            modelBuilder.HasSequence("RBX_RETAILS_UPDATE_CUSTOM_DATA_SEQ");

            modelBuilder.HasSequence("RBX_ROLE_PERMISSIONS_SEQ");

            modelBuilder.HasSequence("RBX_ROLES_SEQ");

            modelBuilder.HasSequence("RBX_S_AIRPORT_LIST_SEQ");

            modelBuilder.HasSequence("RBX_S_AUTHENTICATION_SEQ");

            modelBuilder.HasSequence("RBX_S_CENTILI_DEBIT_TOKEN_REQ_SEQ");

            modelBuilder.HasSequence("RBX_S_EMAIL_MESSAGING_SEQ");

            modelBuilder.HasSequence("RBX_S_MANDATE_BLOB_LOG_SEQ");

            modelBuilder.HasSequence("RBX_S_MANDATE_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_S_MASTERCARD_BENFICIARIES_SEQ");

            modelBuilder.HasSequence("RBX_S_NIP_ACCT_NAME_SEQ");

            modelBuilder.HasSequence("RBX_S_NIPOUT_REVERSAL_SEQ");

            modelBuilder.HasSequence("RBX_S_OTP_DETAIL_SEQ");

            modelBuilder.HasSequence("RBX_S_QUICKTELLER_BILLER_LIST_CATEGORY_SEQUENCE");

            modelBuilder.HasSequence("RBX_S_QUICKTELLER_BILLER_LIST_SEQUENCE");

            modelBuilder.HasSequence("RBX_S_SMS_MESSAGING_SEQ");

            modelBuilder.HasSequence("RBX_S_T_T");

            modelBuilder.HasSequence("RBX_S_TRANSACTION_PIN_PROFILE_SEQ");

            modelBuilder.HasSequence("RBX_S_TSA_COLL_CREDENTIAL");

            modelBuilder.HasSequence("RBX_S_UAE_VISA_PACKAGES_REQ_SEQ");

            modelBuilder.HasSequence("RBX_S_USER_DETAIL_SEQUENCE");

            modelBuilder.HasSequence("RBX_S_USSD_ACCOUNTS_SEQ");

            modelBuilder.HasSequence("RBX_S_USSD_CIF_EXEMPTION_SEQ");

            modelBuilder.HasSequence("RBX_S_USSD_SERVICES_SEQ");

            modelBuilder.HasSequence("RBX_S4_BOT_SEQ");

            modelBuilder.HasSequence("RBX_SALESFORCE_CLIENT_PROD_SEQ");

            modelBuilder.HasSequence("RBX_SALESFORCE_CLIENT_REL_SEQ");

            modelBuilder.HasSequence("RBX_SALESFORCE_CLIENT_SEQ");

            modelBuilder.HasSequence("RBX_SALESFORCE_CONTACT_SEQ");

            modelBuilder.HasSequence("RBX_SALESFORCE_REQ_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SANEF_BVN_VERIF_DET_SEQ");

            modelBuilder.HasSequence("RBX_SANEF_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SANEF_TIERONE_SEQ");

            modelBuilder.HasSequence("RBX_SECRET_WORD_SEQ");

            modelBuilder.HasSequence("RBX_SELFIE_AUTHENTICATION_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SERV_ENTITY_SEQ")
                .IncrementsBy(28)
                .IsCyclic();

            modelBuilder.HasSequence("RBX_SERVICE_VENDING_SEQ");

            modelBuilder.HasSequence("RBX_SETTLEMENT_NOTIF_SEQ");

            modelBuilder.HasSequence("RBX_SIM_SWAP_SEQ");

            modelBuilder.HasSequence("RBX_SISL_REQ_DTD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_ID_TRANS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_FACE_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_PHOTO_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_TRANS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_IDENTITY_TRANS_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_JOB_STATUS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_SMILE_MODULE_MAPPING_SEQ");

            modelBuilder.HasSequence("RBX_SOCHITEL_PRODUCT_SEQ");

            modelBuilder.HasSequence("RBX_SOCHITEL_SEQ");

            modelBuilder.HasSequence("RBX_SRC_MAP_SEQ");

            modelBuilder.HasSequence("RBX_T_ACCOUNT_OPENING_LOG_SEQ");

            modelBuilder.HasSequence("RBX_T_AGENCY_BANKING_TRANSACTIONS_LOG_SEQ");

            modelBuilder.HasSequence("RBX_T_BOOKING_IDS_SEQ");

            modelBuilder.HasSequence("RBX_T_CENTILI_PAYMENT_STATUS_NOTIFICATION_REQ_SEQ");

            modelBuilder.HasSequence("RBX_T_CENTILI_PAYMENT_STATUS_NOTIFICATIONREQ_SEQ");

            modelBuilder.HasSequence("RBX_T_CHANNEL_LOG_SEQ");

            modelBuilder.HasSequence("RBX_T_CHANNEL_SEQ");

            modelBuilder.HasSequence("RBX_T_CROSS_BORDER_TRXN_LIMIQ_SEQ");

            modelBuilder.HasSequence("RBX_T_EXCHANGE_SETTLEMENT_SEQ");

            modelBuilder.HasSequence("RBX_T_INTLNX_THRESHOLD_SEQ");

            modelBuilder.HasSequence("RBX_T_OFI_ENQUIRY_DTD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_T_PAYMENT_STATUS_NOTIFICATION_REQ_SEQ");

            modelBuilder.HasSequence("RBX_T_PREMIER_LOTTO_OPER_SEQ");

            modelBuilder.HasSequence("RBX_T_PREMIER_LOTTO_TRAN_SEQ");

            modelBuilder.HasSequence("RBX_T_SME_PIN_MANAGEMENT_LOG_SEQ");

            modelBuilder.HasSequence("RBX_T_USSD_ACCOUNT_TRAIL_SEQ");

            modelBuilder.HasSequence("RBX_T_USSD_AUDIT_TRAIL_SEQ");

            modelBuilder.HasSequence("RBX_T_WAKANOW_BOOKING_DETAILS_SEQ");

            modelBuilder.HasSequence("RBX_TELLERS_SEQ");

            modelBuilder.HasSequence("RBX_THRIVEAPP_BENEFICIARY_SEQ");

            modelBuilder.HasSequence("RBX_THRIVEAPP_SEQ");

            modelBuilder.HasSequence("RBX_TRAN_REF_SEQ");

            modelBuilder.HasSequence("RBX_TRANSFER_SEQ");

            modelBuilder.HasSequence("RBX_TWOFACTOR_SEQ");

            modelBuilder.HasSequence("RBX_USER_ROLES_SEQ");

            modelBuilder.HasSequence("RBX_USER_SEQ").IsCyclic();

            modelBuilder.HasSequence("RBX_USERS_SEQ");

            modelBuilder.HasSequence("RBX_USSD_TRXN_LIMIT_REQ_SEQ");

            modelBuilder.HasSequence("RBX_VB_ACCOUNT_OPENING_SEQ");

            modelBuilder.HasSequence("RBX_VB_CARD_TRXN_DUMP_SEQ");

            modelBuilder.HasSequence("RBX_VB_CARD_TRXN_REF_SEQ");

            modelBuilder.HasSequence("RBX_VB_FIN_XFERTRNADD_LOG_SEQ");

            modelBuilder.HasSequence("RBX_VB_FINACLE_TRF_LOG_SEQ");

            modelBuilder.HasSequence("RBX_VB_INC_VAT_SWP_RPT_SEQ");

            modelBuilder.HasSequence("RBX_VB_REVERSALS_SEQ");

            modelBuilder.HasSequence("RBX_VCCARD_AUDITLOG_SEQ");

            modelBuilder.HasSequence("RBX_VCCARD_FIN_TRAN_SEQ");

            modelBuilder.HasSequence("RBX_VCCARD_TRAN_LOG_SEQ");

            modelBuilder.HasSequence("RBX_VCCARD_UPLOAD_REQ_SEQ");

            modelBuilder.HasSequence("RBX_VCCARD_USERS_SEQ");

            modelBuilder.HasSequence("RBX_WALLET_FUNDING_NOTIFICATION_REQ_SEQ");

            modelBuilder.HasSequence("RBX_WELLAHEALTH_SEQ");

            modelBuilder.HasSequence("REDBOX_2FA_SEQ");

            modelBuilder.HasSequence("REDBOX_AIRTIMEAGGREGATION_SEQ");

            modelBuilder.HasSequence("REDBOX_BILLPAY_SEQ");

            modelBuilder.HasSequence("REDBOX_CARP_CDR_ITEM_ID_SEQ").IsCyclic();

            modelBuilder.HasSequence("REDBOX_CHANNEL_LOG_SEQ");

            modelBuilder.HasSequence("REDBOX_CHANNEL_SEQ");

            modelBuilder.HasSequence("REDBOX_CHN_TRF_NARR_SEQ");

            modelBuilder.HasSequence("REDBOX_CREDITCARD_SEQ");

            modelBuilder.HasSequence("REDBOX_CUSTSERVICE_SEQ");

            modelBuilder.HasSequence("REDBOX_DEVICEFINANCING_SEQ").IsCyclic();

            modelBuilder.HasSequence("REDBOX_DSTV_SEQ");

            modelBuilder.HasSequence("REDBOX_EBILLS_SEQ");

            modelBuilder.HasSequence("REDBOX_EMAIL_SEQ");

            modelBuilder.HasSequence("REDBOX_FINACLE_SEQ");

            modelBuilder.HasSequence("REDBOX_FUNDWALLET_LOG_SEQ");

            modelBuilder.HasSequence("REDBOX_INFOWARE_SEQ");

            modelBuilder.HasSequence("REDBOX_INTELLINX_LOG_SEQ");

            modelBuilder.HasSequence("REDBOX_MAUTECH_ENQUIRY_SEQ");

            modelBuilder.HasSequence("REDBOX_MAUTECH_ENQUIRY_SEQ2");

            modelBuilder.HasSequence("REDBOX_MAUTECH_SEQ");

            modelBuilder.HasSequence("REDBOX_MFB_SEQ");

            modelBuilder.HasSequence("REDBOX_MOBILEMONEY_ATM_SEQ");

            modelBuilder.HasSequence("REDBOX_MOBILEMONEY_SEQ");

            modelBuilder.HasSequence("REDBOX_MSG_TEMPLATE_SEQ");

            modelBuilder.HasSequence("REDBOX_NAPSIN_SEQ");

            modelBuilder.HasSequence("REDBOX_NAPSINBOUND_SEQ");

            modelBuilder.HasSequence("REDBOX_NIP_INBOUND_SEQ");

            modelBuilder.HasSequence("REDBOX_OTP_ENGINE_CHANNEL_SEQ");

            modelBuilder.HasSequence("REDBOX_OTP_ENGINE_SEQ");

            modelBuilder.HasSequence("REDBOX_PREPAID_CARD_SEQ");

            modelBuilder.HasSequence("REDBOX_QUICKTELLER_ENQ_SEQ");

            modelBuilder.HasSequence("REDBOX_QUICKTELLER_SEQ");

            modelBuilder.HasSequence("REDBOX_RCCG_USSD_SEQ");

            modelBuilder.HasSequence("REDBOX_REMEDY_SEQ");

            modelBuilder.HasSequence("REDBOX_REMITAGOVCOLLECT_SEQ");

            modelBuilder.HasSequence("REDBOX_REQ_MGR_SEQ");

            modelBuilder.HasSequence("REDBOX_REQ_MGR_TNX_LMT_SEQ");

            modelBuilder.HasSequence("REDBOX_SEQ");

            modelBuilder.HasSequence("REDBOX_SMS_SEQ");

            modelBuilder.HasSequence("REDBOX_SSSBANKING_SEQ");

            modelBuilder.HasSequence("REDBOX_STARTIMES_SEQ");

            modelBuilder.HasSequence("REDBOX_T_XPRESSPAY_LOG_SEQ");

            modelBuilder.HasSequence("REDBOX_T_XPRESSPAY_SEQ");

            modelBuilder.HasSequence("REDBOX_TWOFA_SEQ");

            modelBuilder.HasSequence("REDBOX_UI_SEQ");

            modelBuilder.HasSequence("REDBOX_VB_REG_CARD_SEQ");

            modelBuilder.HasSequence("REDBOX_VFX_SEQ");

            modelBuilder.HasSequence("REDBOX_WAEC_SEQ");

            modelBuilder.HasSequence("SEQ_CARP_CDR_ITEM_ID").IsCyclic();

            modelBuilder.HasSequence("SEQUENCE1");

            modelBuilder.HasSequence("SQ_ACCOUNT_DETAILS");

            modelBuilder.HasSequence("SSS_COMMAND_CONFIG_SEQ");

            modelBuilder.HasSequence("SSS_INDIVIDUAL_COMMAND_SEQ").IncrementsBy(50);

            modelBuilder.HasSequence("USSD_ACCT_EXEMPTION_1_SEQ");

            modelBuilder.HasSequence("USSD_CIF_EXEMPTION_1_SEQ");

            modelBuilder.HasSequence("USSD_PAYROLL_LENDING_ID_SEQ");

            modelBuilder.HasSequence("VIRTUAL_BANKING_CORE_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

