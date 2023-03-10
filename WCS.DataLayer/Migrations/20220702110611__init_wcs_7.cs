using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CivilContract_InqViolationState",
                table: "WCS_CivilContract",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CivilContract_InqViolationState",
                table: "WCS_CivilContract");
        }
    }
}
