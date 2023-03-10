using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CivilContract_IsOwnerCar",
                table: "WCS_CivilContract",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CivilContract_IsOwnerCarDesc",
                table: "WCS_CivilContract",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CivilContract_IsOwnerCar",
                table: "WCS_CivilContract");

            migrationBuilder.DropColumn(
                name: "CivilContract_IsOwnerCarDesc",
                table: "WCS_CivilContract");
        }
    }
}
