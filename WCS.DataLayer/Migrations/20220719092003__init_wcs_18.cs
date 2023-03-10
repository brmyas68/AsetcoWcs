using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_OrderCustomer");

            migrationBuilder.DropTable(
                name: "WCS_OrderProducts");

            migrationBuilder.CreateTable(
                name: "WCS_Order",
                columns: table => new
                {
                    Order_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_UserID = table.Column<int>(type: "int", nullable: false),
                    Order_ProductID = table.Column<int>(type: "int", nullable: false),
                    Order_DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_Desc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Order_Count = table.Column<int>(type: "int", nullable: false),
                    Order_Price = table.Column<long>(type: "bigint", nullable: false),
                    Order_ResultComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Order", x => x.Order_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_Order");

            migrationBuilder.CreateTable(
                name: "WCS_OrderCustomer",
                columns: table => new
                {
                    OrderCustomer_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCustomer_FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderCustomer_LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderCustomer_Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_OrderCustomer", x => x.OrderCustomer_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_OrderProducts",
                columns: table => new
                {
                    OrderProducts_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderProducts_Count = table.Column<int>(type: "int", nullable: false),
                    OrderProducts_DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderProducts_Desc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderProducts_OrderCustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderProducts_Price = table.Column<long>(type: "bigint", nullable: false),
                    OrderProducts_ProductID = table.Column<int>(type: "int", nullable: false),
                    OrderProducts_ResultComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_OrderProducts", x => x.OrderProducts_ID);
                });
        }
    }
}
