using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WornCars_CityID",
                table: "WCS_WornCars");

            migrationBuilder.DropColumn(
                name: "WornCars_ProvinceID",
                table: "WCS_WornCars");

            migrationBuilder.AddColumn<string>(
                name: "CivilContract_DocumentName",
                table: "WCS_CivilContract",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CivilContract_DocumentName",
                table: "WCS_CivilContract");

            migrationBuilder.AddColumn<int>(
                name: "WornCars_CityID",
                table: "WCS_WornCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WornCars_ProvinceID",
                table: "WCS_WornCars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
