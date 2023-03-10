using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WCS_Message",
                columns: table => new
                {
                    Message_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message_FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message_Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Message_Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Message_TenantID = table.Column<int>(type: "int", nullable: false),
                    Message_DateSend = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Message", x => x.Message_ID);
                });

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
                    OrderProducts_OrderCustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderProducts_ProductID = table.Column<int>(type: "int", nullable: false),
                    OrderProducts_DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderProducts_Desc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrderProducts_Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_OrderProducts", x => x.OrderProducts_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_Message");

            migrationBuilder.DropTable(
                name: "WCS_OrderCustomer");

            migrationBuilder.DropTable(
                name: "WCS_OrderProducts");
        }
    }
}
