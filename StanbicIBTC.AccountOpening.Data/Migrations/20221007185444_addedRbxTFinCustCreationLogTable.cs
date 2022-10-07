using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanbicIBTC.AccountOpening.Data.Migrations
{
    public partial class addedRbxTFinCustCreationLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RBX_T_FIN_CUST_CREATION_LOG",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", precision: 19, nullable: false),
                    SRC_MODULE_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LIFE_TIME_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SRC_TRAN_REF = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CHANNEL_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    REQUEST_UUID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SERVICE_REQUEST_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SALUTATION = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIRST_NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MIDDLE_NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LAST_NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EMAIL_ADDRESS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN_ENROLLMENT_BANK = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN_ENROLLMENT_BRANCH = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BVN_LINKAGE_FLAG = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PHYSICAL_ADDRESS_DETAILS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DATE_OF_BIRTH = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    GENDER = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    OCCUPATION_CODE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_STAFF_FLAG = table.Column<int>(type: "int", precision: 10, nullable: true),
                    STAFF_EMPLOYEE_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SEGMENTATION_CLASS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SUB_SEGMENT = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ACCOUNT_MANAGER = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LANGUAGE_CODES = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RELATIONSHIP_OPENING_DATE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TAX_DEDUCTION_TABLE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MARITAL_STATUS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NATIONALITY = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EMPLOYMENT_STATUS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SUPPORTING_DOCUMENT_DATA = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CUSTOMER_TYPE_CODE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IS_CUSTOMER_NON_RESIDENT = table.Column<int>(type: "int", precision: 10, nullable: true),
                    CUSTOMER_BRANCH_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ENTITY = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CUST_CORE_MIG_STATUS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CUST_CORE_MIG_DESC = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RESP_CODE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RESP_DESC = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RESP_STATUS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIN_RESP_CODE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIN_RESP_DESC = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIN_RESP_STATUS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIN_ERROR_SRC = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FIN_ERROR_TYPE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PROD_REQ_IN_TIME = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    PROD_REQ_OUT_TIME = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    PROD_RESP_IN_TIME = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    PROD_RESP_OUT_TIME = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    COMPONENT_SERVER_IP = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TRAN_DATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    TRAN_TIME = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RBX_T_FIN_CUST_CREATION_LOG", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RBX_T_FIN_CUST_CREATION_LOG");
        }
    }
}
