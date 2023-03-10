using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    public partial class _init_wcs_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "WCS_Business",
                columns: table => new
                {
                    Business_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Business_MinPrice = table.Column<int>(type: "int", nullable: false),
                    Business_MaxPrice = table.Column<int>(type: "int", nullable: false),
                    Business_SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Business_SplitWornCenterID = table.Column<int>(type: "int", nullable: false),
                    Business_ParkingWornCenterID = table.Column<int>(type: "int", nullable: false),
                    Business_SplitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Business_SplitDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Business_ParkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Business_ParkingDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Business_AgreementAmount = table.Column<int>(type: "int", nullable: false),
                    Business_PreAgreementAmount = table.Column<int>(type: "int", nullable: false),
                    Business_AgreementDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Business", x => x.Business_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_CivilContract",
                columns: table => new
                {
                    CivilContract_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CivilContract_InqDocumentState = table.Column<bool>(type: "bit", nullable: false),
                    CivilContract_InqDocumentDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CivilContract_InqViolationAmount = table.Column<int>(type: "int", nullable: false),
                    CivilContract_InqBlockState = table.Column<bool>(type: "bit", nullable: false),
                    CivilContract_InqPoliceDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CivilContract_InqValidationIdentifyNumber = table.Column<bool>(type: "bit", nullable: false),
                    CivilContract_InqValidationMobile = table.Column<bool>(type: "bit", nullable: false),
                    CivilContract_InqValidationDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CivilContract_Amount = table.Column<int>(type: "int", nullable: false),
                    CivilContract_PreAmount = table.Column<int>(type: "int", nullable: false),
                    CivilContract_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CivilContract_Desc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CivilContract_InqDocumentModifirID = table.Column<int>(type: "int", nullable: false),
                    CivilContract_InqDocumentModifirDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CivilContract_InqPoliceModifirID = table.Column<int>(type: "int", nullable: false),
                    CivilContract_InqPoliceModifirDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CivilContract_InqValidationModifirID = table.Column<int>(type: "int", nullable: false),
                    CivilContract_InqValidationModifirDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_CivilContract", x => x.CivilContract_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_Finance",
                columns: table => new
                {
                    Finance_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finance_WornMasterID = table.Column<int>(type: "int", nullable: false),
                    Finance_RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finance_PaymentType = table.Column<short>(type: "smallint", nullable: false),
                    Finance_Amount = table.Column<int>(type: "int", nullable: false),
                    Finance_Desc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Finance_ModifirID = table.Column<int>(type: "int", nullable: false),
                    Finance_ModifirDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Finance", x => x.Finance_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_ModelCar",
                columns: table => new
                {
                    ModelCar_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelCar_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelCar_BrandCarID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_ModelCar", x => x.ModelCar_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_Owners",
                columns: table => new
                {
                    Owners_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owners_UserID = table.Column<int>(type: "int", nullable: false),
                    Owners_Desc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Owners_ShabaNumber = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_Owners", x => x.Owners_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_WornCars",
                columns: table => new
                {
                    WornCars_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WornCars_BrandID = table.Column<int>(type: "int", nullable: false),
                    WornCars_ModelID = table.Column<int>(type: "int", nullable: false),
                    WornCars_UserType = table.Column<short>(type: "smallint", nullable: false),
                    WornCars_PelakType = table.Column<short>(type: "smallint", nullable: false),
                    WornCars_BuildYear = table.Column<int>(type: "int", nullable: false),
                    WornCars_State = table.Column<short>(type: "smallint", nullable: false),
                    WornCars_ProvinceID = table.Column<int>(type: "int", nullable: false),
                    WornCars_CityID = table.Column<int>(type: "int", nullable: false),
                    WornCars_Desc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_WornCars", x => x.WornCars_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_WornCenter",
                columns: table => new
                {
                    WornCenter_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WornCenter_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WornCenter_ProvinceID = table.Column<int>(type: "int", nullable: false),
                    WornCenter_CityID = table.Column<int>(type: "int", nullable: false),
                    WornCenter_Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WornCenter_Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WornCenter_Tell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WornCenter_Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WornCenter_ManagerFullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    WornCenter_Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_WornCenter", x => x.WornCenter_ID);
                });

            migrationBuilder.CreateTable(
                name: "WCS_WornMaster",
                columns: table => new
                {
                    WornMaster_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WornMaster_OwnerID = table.Column<int>(type: "int", nullable: false),
                    WornMaster_WornCarID = table.Column<int>(type: "int", nullable: false),
                    WornMaster_AgentID = table.Column<int>(type: "int", nullable: true),
                    WornMaster_CivilContractID = table.Column<int>(type: "int", nullable: true),
                    WornMaster_BusinessID = table.Column<int>(type: "int", nullable: true),
                    WornMaster_RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WornMaster_TrackingCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    WornMaster_State = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCS_WornMaster", x => x.WornMaster_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WCS_BrandCar");

            migrationBuilder.DropTable(
                name: "WCS_Business");

            migrationBuilder.DropTable(
                name: "WCS_CivilContract");

            migrationBuilder.DropTable(
                name: "WCS_Finance");

            migrationBuilder.DropTable(
                name: "WCS_ModelCar");

            migrationBuilder.DropTable(
                name: "WCS_Owners");

            migrationBuilder.DropTable(
                name: "WCS_WornCars");

            migrationBuilder.DropTable(
                name: "WCS_WornCenter");

            migrationBuilder.DropTable(
                name: "WCS_WornMaster");
        }
    }
}
