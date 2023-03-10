using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _Init_25_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order_OrderCode",
                table: "WCS_Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WCS_OrderTransaction",
                columns: table => new
                {
                    OrderTransaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTransaction_UserID = table.Column<int>(type: "int", nullable: false),
                    OrderTransaction_DateTrans = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTransaction_DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTransaction_TrackingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_OrderTransaction", x => x.OrderTransaction_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_OrderTransaction");

            migrationBuilder.DropColumn(
                name: "Order_OrderCode",
                table: "WCS_Order");
        }
    }
}
