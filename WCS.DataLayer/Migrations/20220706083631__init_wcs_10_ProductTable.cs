using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_10_ProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WCS_Product",
                columns: table => new
                {
                    Products_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Products_Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Products_Model = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Products_Series = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Products_Price = table.Column<long>(type: "bigint", nullable: false),
                    Products_Group = table.Column<short>(type: "smallint", nullable: false),
                    Products_Type = table.Column<short>(type: "smallint", nullable: false),
                    Products_IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Products_Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Product", x => x.Products_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_Product");
        }
    }
}
