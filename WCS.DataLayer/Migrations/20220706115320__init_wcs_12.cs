using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Products_Type",
                table: "WCS_Product",
                newName: "Product_Type");

            migrationBuilder.RenameColumn(
                name: "Products_Series",
                table: "WCS_Product",
                newName: "Product_Series");

            migrationBuilder.RenameColumn(
                name: "Products_RegisterDate",
                table: "WCS_Product",
                newName: "Product_RegisterDate");

            migrationBuilder.RenameColumn(
                name: "Products_Price",
                table: "WCS_Product",
                newName: "Product_Price");

            migrationBuilder.RenameColumn(
                name: "Products_Name",
                table: "WCS_Product",
                newName: "Product_Name");

            migrationBuilder.RenameColumn(
                name: "Products_Model",
                table: "WCS_Product",
                newName: "Product_Model");

            migrationBuilder.RenameColumn(
                name: "Products_IsUsed",
                table: "WCS_Product",
                newName: "Product_IsUsed");

            migrationBuilder.RenameColumn(
                name: "Products_Group",
                table: "WCS_Product",
                newName: "Product_Group");

            migrationBuilder.RenameColumn(
                name: "Products_Description",
                table: "WCS_Product",
                newName: "Product_Description");

            migrationBuilder.RenameColumn(
                name: "Products_ID",
                table: "WCS_Product",
                newName: "Product_ID");

            migrationBuilder.AddColumn<int>(
                name: "Business_Investor",
                table: "WCS_Business",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Business_Investor",
                table: "WCS_Business");

            migrationBuilder.RenameColumn(
                name: "Product_Type",
                table: "WCS_Product",
                newName: "Products_Type");

            migrationBuilder.RenameColumn(
                name: "Product_Series",
                table: "WCS_Product",
                newName: "Products_Series");

            migrationBuilder.RenameColumn(
                name: "Product_RegisterDate",
                table: "WCS_Product",
                newName: "Products_RegisterDate");

            migrationBuilder.RenameColumn(
                name: "Product_Price",
                table: "WCS_Product",
                newName: "Products_Price");

            migrationBuilder.RenameColumn(
                name: "Product_Name",
                table: "WCS_Product",
                newName: "Products_Name");

            migrationBuilder.RenameColumn(
                name: "Product_Model",
                table: "WCS_Product",
                newName: "Products_Model");

            migrationBuilder.RenameColumn(
                name: "Product_IsUsed",
                table: "WCS_Product",
                newName: "Products_IsUsed");

            migrationBuilder.RenameColumn(
                name: "Product_Group",
                table: "WCS_Product",
                newName: "Products_Group");

            migrationBuilder.RenameColumn(
                name: "Product_Description",
                table: "WCS_Product",
                newName: "Products_Description");

            migrationBuilder.RenameColumn(
                name: "Product_ID",
                table: "WCS_Product",
                newName: "Products_ID");
        }
    }
}
