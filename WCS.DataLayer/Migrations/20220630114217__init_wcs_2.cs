using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "WornCars_DocumentType",
                table: "WCS_WornCars",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WornCars_Weight",
                table: "WCS_WornCars",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WornCars_DocumentType",
                table: "WCS_WornCars");

            migrationBuilder.DropColumn(
                name: "WornCars_Weight",
                table: "WCS_WornCars");
        }
    }
}
