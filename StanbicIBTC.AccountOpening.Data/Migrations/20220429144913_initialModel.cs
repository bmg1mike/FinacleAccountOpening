using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanbicIBTC.AccountOpening.Data.Migrations
{
    public partial class initialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_BVN_LINKAGE_LOG",
                table: "T_BVN_LINKAGE_LOG");

            migrationBuilder.DropPrimaryKey(
                name: "RETAILS_UPDATE_CUSTOM_DATA_PK",
                table: "RETAILS_UPDATE_CUSTOM_DATA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BPM_NEXT_OF_KIN_DETAILS",
                table: "BPM_NEXT_OF_KIN_DETAILS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BPM_CORP_CIF_CUSTOM_DATA",
                table: "BPM_CORP_CIF_CUSTOM_DATA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BPM_CIF_CUSTOM_DATA",
                table: "BPM_CIF_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "T_BVN_LINKAGE_LOG",
                newName: "RBX_T_BVN_LINKAGE_LOG");

            migrationBuilder.RenameTable(
                name: "RETAILS_UPDATE_CUSTOM_DATA",
                newName: "RBX_RETAILS_UPDATE_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "BPM_NEXT_OF_KIN_DETAILS",
                newName: "RBX_BPM_NEXT_OF_KIN_DETAILS");

            migrationBuilder.RenameTable(
                name: "BPM_CORP_CIF_CUSTOM_DATA",
                newName: "RBX_BPM_CORP_CIF_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "BPM_CORP_CIF_COMPANY",
                newName: "RBX_BPM_CORP_CIF_COMPANY");

            migrationBuilder.RenameTable(
                name: "BPM_CIF_CUSTOM_DATA",
                newName: "RBX_BPM_CIF_CUSTOM_DATA");

            migrationBuilder.RenameIndex(
                name: "IX_BPM_NEXT_OF_KIN_DETAILS_FK_CUSTOMER_ADD_DETAILS",
                table: "RBX_BPM_NEXT_OF_KIN_DETAILS",
                newName: "IX_RBX_BPM_NEXT_OF_KIN_DETAILS_FK_CUSTOMER_ADD_DETAILS");

            migrationBuilder.RenameIndex(
                name: "IX_BPM_CORP_CIF_COMPANY_FK_CUSTOM_DATA",
                table: "RBX_BPM_CORP_CIF_COMPANY",
                newName: "IX_RBX_BPM_CORP_CIF_COMPANY_FK_CUSTOM_DATA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RBX_T_BVN_LINKAGE_LOG",
                table: "RBX_T_BVN_LINKAGE_LOG",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "RBX_RETAILS_UPDATE_CUSTOM_DATA_PK",
                table: "RBX_RETAILS_UPDATE_CUSTOM_DATA",
                column: "CUSTOMER_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RBX_BPM_NEXT_OF_KIN_DETAILS",
                table: "RBX_BPM_NEXT_OF_KIN_DETAILS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RBX_BPM_CORP_CIF_CUSTOM_DATA",
                table: "RBX_BPM_CORP_CIF_CUSTOM_DATA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RBX_BPM_CIF_CUSTOM_DATA",
                table: "RBX_BPM_CIF_CUSTOM_DATA",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RBX_T_BVN_LINKAGE_LOG",
                table: "RBX_T_BVN_LINKAGE_LOG");

            migrationBuilder.DropPrimaryKey(
                name: "RBX_RETAILS_UPDATE_CUSTOM_DATA_PK",
                table: "RBX_RETAILS_UPDATE_CUSTOM_DATA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RBX_BPM_NEXT_OF_KIN_DETAILS",
                table: "RBX_BPM_NEXT_OF_KIN_DETAILS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RBX_BPM_CORP_CIF_CUSTOM_DATA",
                table: "RBX_BPM_CORP_CIF_CUSTOM_DATA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RBX_BPM_CIF_CUSTOM_DATA",
                table: "RBX_BPM_CIF_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "RBX_T_BVN_LINKAGE_LOG",
                newName: "T_BVN_LINKAGE_LOG");

            migrationBuilder.RenameTable(
                name: "RBX_RETAILS_UPDATE_CUSTOM_DATA",
                newName: "RETAILS_UPDATE_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "RBX_BPM_NEXT_OF_KIN_DETAILS",
                newName: "BPM_NEXT_OF_KIN_DETAILS");

            migrationBuilder.RenameTable(
                name: "RBX_BPM_CORP_CIF_CUSTOM_DATA",
                newName: "BPM_CORP_CIF_CUSTOM_DATA");

            migrationBuilder.RenameTable(
                name: "RBX_BPM_CORP_CIF_COMPANY",
                newName: "BPM_CORP_CIF_COMPANY");

            migrationBuilder.RenameTable(
                name: "RBX_BPM_CIF_CUSTOM_DATA",
                newName: "BPM_CIF_CUSTOM_DATA");

            migrationBuilder.RenameIndex(
                name: "IX_RBX_BPM_NEXT_OF_KIN_DETAILS_FK_CUSTOMER_ADD_DETAILS",
                table: "BPM_NEXT_OF_KIN_DETAILS",
                newName: "IX_BPM_NEXT_OF_KIN_DETAILS_FK_CUSTOMER_ADD_DETAILS");

            migrationBuilder.RenameIndex(
                name: "IX_RBX_BPM_CORP_CIF_COMPANY_FK_CUSTOM_DATA",
                table: "BPM_CORP_CIF_COMPANY",
                newName: "IX_BPM_CORP_CIF_COMPANY_FK_CUSTOM_DATA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_BVN_LINKAGE_LOG",
                table: "T_BVN_LINKAGE_LOG",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "RETAILS_UPDATE_CUSTOM_DATA_PK",
                table: "RETAILS_UPDATE_CUSTOM_DATA",
                column: "CUSTOMER_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BPM_NEXT_OF_KIN_DETAILS",
                table: "BPM_NEXT_OF_KIN_DETAILS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BPM_CORP_CIF_CUSTOM_DATA",
                table: "BPM_CORP_CIF_CUSTOM_DATA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BPM_CIF_CUSTOM_DATA",
                table: "BPM_CIF_CUSTOM_DATA",
                column: "ID");
        }
    }
}
