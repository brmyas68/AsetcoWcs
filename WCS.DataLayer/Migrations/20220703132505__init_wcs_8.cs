using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WornCars_BrandID",
                table: "WCS_WornCars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WornCars_BrandID",
                table: "WCS_WornCars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
