using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WCS_ProductPrice",
                columns: table => new
                {
                    ProductPrice_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPrice_ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductPrice_Price = table.Column<long>(type: "bigint", nullable: false),
                    ProductPrice_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_ProductPrice", x => x.ProductPrice_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_ProductPrice");
        }
    }
}
