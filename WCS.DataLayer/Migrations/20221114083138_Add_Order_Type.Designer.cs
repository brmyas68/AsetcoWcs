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
    [Migration("20221114083138_Add_Order_Type")]
    partial class Add_Order_Type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WCS.ClassDomain.Domains.Business", b =>
                {
                    b.Property<int>("Business_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Business_ID"), 1L, 1);

                    b.Property<long?>("Business_AgreementAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("Business_AgreementDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("Business_Investor")
                        .HasColumnType("int");

                    b.Property<long?>("Business_MaxPrice")
                        .HasColumnType("bigint");

                    b.Property<long?>("Business_MinPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Business_ParkingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Business_ParkingDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("Business_ParkingWornCenterID")
                        .HasColumnType("int");

                    b.Property<long?>("Business_PreAgreementAmount")
                        .HasColumnType("bigint");

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

                    b.Property<long?>("CivilContract_Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CivilContract_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("CivilContract_Desc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CivilContract_DocumentName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("CivilContract_InqBlockState")
                        .HasColumnType("bit");

                    b.Property<string>("CivilContract_InqDocumentDesc")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("CivilContract_InqDocumentModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CivilContract_InqDocumentModifirID")
                        .HasColumnType("int");

                    b.Property<bool?>("CivilContract_InqDocumentState")
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

                    b.Property<bool?>("CivilContract_InqValidationIdentifyNumber")
                        .HasColumnType("bit");

                    b.Property<bool?>("CivilContract_InqValidationMobile")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CivilContract_InqValidationModifirDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CivilContract_InqValidationModifirID")
                        .HasColumnType("int");

                    b.Property<long?>("CivilContract_InqViolationAmount")
                        .HasColumnType("bigint");

                    b.Property<bool?>("CivilContract_InqViolationState")
                        .HasColumnType("bit");

                    b.Property<bool?>("CivilContract_IsOwnerCar")
                        .HasColumnType("bit");

                    b.Property<string>("CivilContract_IsOwnerCarDesc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("CivilContract_PreAmount")
                        .HasColumnType("bigint");

                    b.HasKey("CivilContract_ID");

                    b.ToTable("WCS_CivilContract", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Finance", b =>
                {
                    b.Property<int>("Finance_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Finance_ID"), 1L, 1);

                    b.Property<long?>("Finance_Amount")
                        .HasColumnType("bigint");

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

            modelBuilder.Entity("WCS.ClassDomain.Domains.Message", b =>
                {
                    b.Property<int>("Message_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Message_ID"), 1L, 1);

                    b.Property<DateTime?>("Message_DateSend")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message_FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Message_Mobile")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("Message_TenantID")
                        .HasColumnType("int");

                    b.Property<string>("Message_Text")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Message_ID");

                    b.ToTable("WCS_Message", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.Order", b =>
                {
                    b.Property<int>("Order_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_ID"), 1L, 1);

                    b.Property<int>("Order_Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Order_DateRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("Order_Desc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("Order_OrderCode")
                        .HasColumnType("int");

                    b.Property<long>("Order_Price")
                        .HasColumnType("bigint");

                    b.Property<int>("Order_ProductID")
                        .HasColumnType("int");

                    b.Property<short?>("Order_ResultComment")
                        .HasColumnType("smallint");

                    b.Property<short>("Order_Type")
                        .HasColumnType("smallint");

                    b.Property<int>("Order_UserID")
                        .HasColumnType("int");

                    b.HasKey("Order_ID");

                    b.ToTable("WCS_Order", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.OrderTransaction", b =>
                {
                    b.Property<int>("OrderTransaction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderTransaction_ID"), 1L, 1);

                    b.Property<DateTime>("OrderTransaction_DateTrans")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderTransaction_OrderCode")
                        .HasColumnType("int");

                    b.Property<long>("OrderTransaction_Price")
                        .HasColumnType("bigint");

                    b.Property<string>("OrderTransaction_TrackingCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrderTransaction_UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderTransaction_ID");

                    b.ToTable("WCS_OrderTransaction", (string)null);
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
                    b.Property<int>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_ID"), 1L, 1);

                    b.Property<string>("Product_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Product_Group")
                        .HasColumnType("smallint");

                    b.Property<bool>("Product_IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Product_Model")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Product_NameEn")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long>("Product_Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Product_RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Product_Series")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<short>("Product_Type")
                        .HasColumnType("smallint");

                    b.HasKey("Product_ID");

                    b.ToTable("WCS_Product", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.ProductGroup", b =>
                {
                    b.Property<int>("ProductGroup_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductGroup_ID"), 1L, 1);

                    b.Property<string>("ProductGroup_Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<short>("ProductGroup_Type")
                        .HasColumnType("smallint");

                    b.HasKey("ProductGroup_ID");

                    b.ToTable("WCS_ProductGroup", (string)null);
                });

            modelBuilder.Entity("WCS.ClassDomain.Domains.ProductPrice", b =>
                {
                    b.Property<int>("ProductPrice_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductPrice_ID"), 1L, 1);

                    b.Property<DateTime>("ProductPrice_Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProductPrice_Price")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductPrice_ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductPrice_ID");

                    b.ToTable("WCS_ProductPrice", (string)null);
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

                    b.Property<bool>("WornCars_SellType")
                        .HasColumnType("bit");

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

                    b.Property<bool>("WornMaster_WornCarState")
                        .HasColumnType("bit");

                    b.HasKey("WornMaster_ID");

                    b.ToTable("WCS_WornMaster", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
