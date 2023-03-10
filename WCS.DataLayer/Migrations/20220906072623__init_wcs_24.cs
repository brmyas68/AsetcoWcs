using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_BrandCar");

            migrationBuilder.DropTable(
                name: "WCS_ModelCar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WCS_BrandCar",
                columns: table => new
                {
                    BrandCar_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandCar_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_BrandCar", x => x.BrandCar_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_ModelCar",
                columns: table => new
                {
                    ModelCar_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelCar_BrandCarID = table.Column<int>(type: "int", nullable: false),
                    ModelCar_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_ModelCar", x => x.ModelCar_ID);
                });
        }
    }
}
