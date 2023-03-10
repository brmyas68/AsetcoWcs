using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WornMaster_StateDesc",
                table: "WCS_WornMaster",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Business_PriceDesc",
                table: "WCS_Business",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WornMaster_StateDesc",
                table: "WCS_WornMaster");

            migrationBuilder.DropColumn(
                name: "Business_PriceDesc",
                table: "WCS_Business");
        }
    }
}
