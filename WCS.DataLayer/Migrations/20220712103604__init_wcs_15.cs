using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderProducts_Price",
                table: "WCS_OrderProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderProducts_Price",
                table: "WCS_OrderProducts");
        }
    }
}
