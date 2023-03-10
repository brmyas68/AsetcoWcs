using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _Init_26_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTransaction_DateRegister",
                table: "WCS_OrderTransaction");

            migrationBuilder.AddColumn<int>(
                name: "OrderTransaction_OrderCode",
                table: "WCS_OrderTransaction",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTransaction_OrderCode",
                table: "WCS_OrderTransaction");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTransaction_DateRegister",
                table: "WCS_OrderTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
