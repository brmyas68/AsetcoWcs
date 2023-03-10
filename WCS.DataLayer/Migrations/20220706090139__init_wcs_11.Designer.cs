﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WCS.DataLayer.Contex;

#nullable disable

namespace WCS.DataLayer.Migrations
{
    [DbContext(typeof(ContextWCS))]
    [Migration("20220706090139__init_wcs_11")]
    partial class _init_wcs_11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WCS.ClassDomain.Domains.BrandCar", b =>
                {
                    b.Property<int>("BrandCar_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandCar_ID"), 1L, 1);

                    b.Property<string>("BrandCar_Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("BrandCar_ID");

                    b.ToTable("WCS_BrandCar", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Business", b =>
                {
                    b.Property<int>("Business_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Business_ID"), 1L, 1);

                    b.Property<int?>("Business_AgreementAmount")
                        .HasColumnType("int");

                    b.Property<string>("Business_AgreementDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("Business_MaxPrice")
                        .HasColumnType("int");

                    b.Property<int?>("Business_MinPrice")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Business_ParkingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Business_ParkingDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("Business_ParkingWornCenterID")
                        .HasColumnType("int");

                    b.Property<int?>("Business_PreAgreementAmount")
                        .HasColumnType("int");

                    b.Property<string>("Business_PriceDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("Business_SendDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Business_SplitDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Business_SplitDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("Business_SplitWornCenterID")
                        .HasColumnType("int");

                    b.HasKey("Business_ID");

                    b.ToTable("WCS_Business", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.CivilContract", b =>
                {
                    b.Property<int>("CivilContract_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CivilContract_ID"), 1L, 1);

                    b.Property<int?>("CivilContract_Amount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CivilContract_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("CivilContract_Desc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CivilContract_DocumentName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("CivilContract_InqBlockState")
                        .HasColumnType("bit");

                    b.Property<string>("CivilContract_InqDocumentDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("CivilContract_InqDocumentModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CivilContract_InqDocumentModifirID")
                        .HasColumnType("int");

                    b.Property<bool>("CivilContract_InqDocumentState")
                        .HasColumnType("bit");

                    b.Property<string>("CivilContract_InqPoliceDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("CivilContract_InqPoliceModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CivilContract_InqPoliceModifirID")
                        .HasColumnType("int");

                    b.Property<string>("CivilContract_InqValidationDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("CivilContract_InqValidationIdentifyNumber")
                        .HasColumnType("bit");

                    b.Property<bool>("CivilContract_InqValidationMobile")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CivilContract_InqValidationModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CivilContract_InqValidationModifirID")
                        .HasColumnType("int");

                    b.Property<int?>("CivilContract_InqViolationAmount")
                        .HasColumnType("int");

                    b.Property<bool>("CivilContract_InqViolationState")
                        .HasColumnType("bit");

                    b.Property<bool>("CivilContract_IsOwnerCar")
                        .HasColumnType("bit");

                    b.Property<string>("CivilContract_IsOwnerCarDesc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("CivilContract_PreAmount")
                        .HasColumnType("int");

                    b.HasKey("CivilContract_ID");

                    b.ToTable("WCS_CivilContract", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Finance", b =>
                {
                    b.Property<int>("Finance_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Finance_ID"), 1L, 1);

                    b.Property<int?>("Finance_Amount")
                        .HasColumnType("int");

                    b.Property<string>("Finance_Desc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("Finance_ModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Finance_ModifirID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<short>("Finance_PaymentType")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("Finance_RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Finance_WornMasterID")
                        .HasColumnType("int");

                    b.HasKey("Finance_ID");

                    b.ToTable("WCS_Finance", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.ModelCar", b =>
                {
                    b.Property<int>("ModelCar_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelCar_ID"), 1L, 1);

                    b.Property<int>("ModelCar_BrandCarID")
                        .HasColumnType("int");

                    b.Property<string>("ModelCar_Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ModelCar_ID");

                    b.ToTable("WCS_ModelCar", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Owners", b =>
                {
                    b.Property<int>("Owners_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Owners_ID"), 1L, 1);

                    b.Property<string>("Owners_Desc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Owners_ShabaNumber")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<string>("Owners_Tell")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Owners_UserID")
                        .HasColumnType("int");

                    b.HasKey("Owners_ID");

                    b.ToTable("WCS_Owners", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Product", b =>
                {
                    b.Property<int>("Products_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Products_ID"), 1L, 1);

                    b.Property<string>("Products_Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<short>("Products_Group")
                        .HasColumnType("smallint");

                    b.Property<bool>("Products_IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Products_Model")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Products_Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long>("Products_Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Products_RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Products_Series")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<short>("Products_Type")
                        .HasColumnType("smallint");

                    b.HasKey("Products_ID");

                    b.ToTable("WCS_Product", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.WornCars", b =>
                {
                    b.Property<int>("WornCars_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WornCars_ID"), 1L, 1);

                    b.Property<int?>("WornCars_BuildYear")
                        .HasColumnType("int");

                    b.Property<string>("WornCars_Desc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<short?>("WornCars_DocumentType")
                        .HasColumnType("smallint");

                    b.Property<int>("WornCars_ModelID")
                        .HasColumnType("int");

                    b.Property<short>("WornCars_PelakType")
                        .HasColumnType("smallint");

                    b.Property<short>("WornCars_State")
                        .HasColumnType("smallint");

                    b.Property<short>("WornCars_UserType")
                        .HasColumnType("smallint");

                    b.Property<int?>("WornCars_Weight")
                        .HasColumnType("int");

                    b.HasKey("WornCars_ID");

                    b.ToTable("WCS_WornCars", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.WornCenter", b =>
                {
                    b.Property<int>("WornCenter_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WornCenter_ID"), 1L, 1);

                    b.Property<string>("WornCenter_Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("WornCenter_CityID")
                        .HasColumnType("int");

                    b.Property<string>("WornCenter_Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("WornCenter_Fax")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WornCenter_ManagerFullName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("WornCenter_Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("WornCenter_ProvinceID")
                        .HasColumnType("int");

                    b.Property<string>("WornCenter_Tell")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<short>("WornCenter_Type")
                        .HasColumnType("smallint");

                    b.HasKey("WornCenter_ID");

                    b.ToTable("WCS_WornCenter", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.WornMaster", b =>
                {
                    b.Property<int>("WornMaster_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WornMaster_ID"), 1L, 1);

                    b.Property<int?>("WornMaster_AgentID")
                        .HasColumnType("int");

                    b.Property<int?>("WornMaster_BusinessID")
                        .HasColumnType("int");

                    b.Property<int?>("WornMaster_CivilContractID")
                        .HasColumnType("int");

                    b.Property<int>("WornMaster_OwnerID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("WornMaster_RegisterDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<short>("WornMaster_State")
                        .HasColumnType("smallint");

                    b.Property<string>("WornMaster_StateDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("WornMaster_TrackingCode")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("WornMaster_WornCarID")
                        .HasColumnType("int");

                    b.HasKey("WornMaster_ID");

                    b.ToTable("WCS_WornMaster", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
