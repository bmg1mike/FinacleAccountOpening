using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanbicIBTC.AccountOpening.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BPM_CIF_CUSTOM_DATA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: false),
                    ACCOMODATION_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ASSET_CLASSIFICATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTER_PARTY_INFO = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_BIRTH = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CUSTOMER_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    EMPLOYMENT_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_CLASSIFICATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FOREIGN_CUSTOMER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IDENTITY_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INDUSTRY_SARB_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_ONLY_NATIONALITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KEY_CONTACT_PERSON = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_CHALLENGE_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_ENTITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LOCAL_INDICIA = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    MARITAL_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    MONTHLY_NET_INCOME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    OCCUPATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    POLITICALLY_EXPOSED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PREFERRED_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRI_COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_NATIONALITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    REGION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RSV_BANK_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RETURNS_CLASS_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RETURNS_CLASSIFICATION_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SECONDARY_RM_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SECONDARY_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SHORT_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    VALIDATE_FNR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WITH_HOLDING_TAX = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NATIONAL_ID_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    BASEL_II_INDICATOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INSIDER_TO_BANK = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KYC_INDICATOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PORTFOLIO_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DISTRIBUTION_CHANNEL = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ONLY_COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TAX_ID_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TERTIARY_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BASE2_INDICATOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BRANCH_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTER_PARTY_INFORMATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DEFAULT_ADDRESS_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IDENTITY_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INDUSTRY_CLASSIFICATION_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_CORE_PROFILE_ACTIVE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_CUSTOMER_MINOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_ONLY_COUNTRY_OF_NATIONALITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_ONLY_COUNTRY_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KEY_CONTACT_PERSON_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_TAX_RESIDENCE_COUNTRY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RELATIONSHIP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RESERVE_BANK_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TAX_IDENTIFICATION_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WITHHOLDING_TAX = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ZIP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPM_CIF_CUSTOM_DATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BPM_CORP_CIF_CUSTOM_DATA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: false),
                    BUSINESS_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CORPORATE_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTER_PARTY_INFO = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CUSTOMER_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CUSTOMER_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    DISTRIBUTION_CHANNEL = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    EARNS_50_PERCENT_GROSS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ENTITY_CLASS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FAT_CA_EXEMPT = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FINANCIAL_INSTITUTION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_CLASSIFICATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FUND_SOURCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    HOLDS_50_PERCENT_GROSS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ID_ISSUED_ORGANIZATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INCOME_TAX_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INDUSTRY_CLASSIFICATION_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INSIDER_TO_BANK = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KEY_CONTACT_PERSON = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KYC_INDICATOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_ENTITY_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    MONTHLY_NET_INCOME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ONLY_COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    POLITICALLY_EXPOSED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PORTFOLIO_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PREFERRED_CONTACT_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_EMAIL_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RELATIONSHIP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RSV_BANK_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RETURNS_CLASS_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SBG_MEMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SCUM_REG_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SECONDARY_RM_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SEC_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    STATE_OF_INC = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    UNIQUE_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    US_RELATED_PARTIES = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    W8_BEN_RECEIVED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WEBSITE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WITHHOLDING_TAX = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_CHALLENGE_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LIST_APPROVED_EXCHANGES = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    REGION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TERTIARY_SIC_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPM_CORP_CIF_CUSTOM_DATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RETAILS_UPDATE_CUSTOM_DATA",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: false),
                    BRANCH_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_CORE_PROFILE_ACTIVE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_CUSTOMER_MINOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DEFAULT_ADDRESS_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ZIP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RELATIONSHIP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    EMPLOYMENT_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    POLITICALLY_EXPOSED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_TAX_RESIDENCE_COUNTRY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ASSET_CLASSIFICATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTER_PARTY_INFORMATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DISTRIBUTION_CHANNEL = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_CLASSIFICATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FNR_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FOREIGN_CUSTOMER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IDENTITY_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    INDUSTRY_CLASSIFICATION_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_ONLY_COUNTRY_OF_NATIONALITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_ONLY_COUNTRY_TAX_RESIDENCE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KEY_CONTACT_PERSON_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    KYC_INDICATOR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_CHALLENGE_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LEGAL_ENTITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LOCAL_INDICIA = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    MONTHLY_NET_INCOME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRIMARY_NATIONALITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RESERVE_BANK_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RETURNS_CLASSIFICATION_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TAX_IDENTIFICATION_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    VALIDATE_FNR = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WITHHOLDING_TAX = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_FIRST_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_LAST_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_MIDDLE_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_GENDER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_DATE_OF_BIRTH = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_EMAIL = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS_FORMAT = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_RELATIONSHIP_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS_LINE1 = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS_LINE2 = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS_LINE3 = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_CITY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_COUNTRY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_PREFERRED_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_PREFERRED_FORMAT = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_STATE_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_STREET_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_LOCALITY_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_ADDRESS_CATEGORY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    NOK_POSTAL_CODE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RETAILS_UPDATE_CUSTOM_DATA_PK", x => x.CUSTOMER_ID);
                });

            migrationBuilder.CreateTable(
                name: "T_BVN_LINKAGE_LOG",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: false),
                    ACCT_NO = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ACCT_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BANK_ENROLLED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BRANCH_ENROLLED = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN_FORM = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    BVN_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: false),
                    BRANCH_NO = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    BRANCH_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    CIF_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: false),
                    PHONE_NO = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RECORD_IS_FINAL_FLAG = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RECORD_DEL_FLAG = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RECORD_HASH = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RECORD_SUBMISSION_FLAG = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    SAPID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TRANSACTION_STATUS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    WKF_ID = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COMPONENT_SERVER_IP = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TRAN_DATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    TRAN_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BVN_LINKAGE_LOG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BPM_NEXT_OF_KIN_DETAILS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: false),
                    ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    ADDRESS_FORMAT = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "DATE", nullable: true),
                    EMAIL_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FIRST_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    GENDER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    LAST_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    MIDDLE_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    RELATIONSHIP_TYPE = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FK_CUSTOMER_ADD_DETAILS = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPM_NEXT_OF_KIN_DETAILS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_A0THOFDBVKI693PXJCNJNJFTI",
                        column: x => x.FK_CUSTOMER_ADD_DETAILS,
                        principalTable: "BPM_CIF_CUSTOM_DATA",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BPM_CORP_CIF_COMPANY",
                columns: table => new
                {
                    COMPANY_ID = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: false),
                    BUSINESS_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_INCORPORATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    PRINCIPAL_OFFICE_COUNTRY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    COUNTRY_OF_REGISTRATION = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    HEAD_OFFICE_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_FOREIGN_COMPANY = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    REGISTERED_ADDRESS = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    REGISTERED_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    REGISTRATION_NUMBER = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    TRADE_NAME = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    FK_CUSTOM_DATA = table.Column<string>(type: "NVARCHAR2(450)", precision: 19, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C0016340", x => x.COMPANY_ID);
                    table.ForeignKey(
                        name: "FK_6CATLOTUA2JAI97SILH40P18S",
                        column: x => x.FK_CUSTOM_DATA,
                        principalTable: "BPM_CORP_CIF_CUSTOM_DATA",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BPM_CORP_CIF_COMPANY_FK_CUSTOM_DATA",
                table: "BPM_CORP_CIF_COMPANY",
                column: "FK_CUSTOM_DATA");

            migrationBuilder.CreateIndex(
                name: "IX_BPM_NEXT_OF_KIN_DETAILS_FK_CUSTOMER_ADD_DETAILS",
                table: "BPM_NEXT_OF_KIN_DETAILS",
                column: "FK_CUSTOMER_ADD_DETAILS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BPM_CORP_CIF_COMPANY");

            migrationBuilder.DropTable(
                name: "BPM_NEXT_OF_KIN_DETAILS");

            migrationBuilder.DropTable(
                name: "RETAILS_UPDATE_CUSTOM_DATA");

            migrationBuilder.DropTable(
                name: "T_BVN_LINKAGE_LOG");

            migrationBuilder.DropTable(
                name: "BPM_CORP_CIF_CUSTOM_DATA");

            migrationBuilder.DropTable(
                name: "BPM_CIF_CUSTOM_DATA");
        }
    }
}
