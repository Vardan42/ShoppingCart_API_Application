﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCart_API_Application.Data;

#nullable disable

namespace ShoppingCart_API_Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230701182347_AddProductNumberToTable")]
    partial class AddProductNumberToTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCart_API_Application.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 1, 11, 23, 47, 479, DateTimeKind.Local).AddTicks(1290),
                            Details = "Samsung Group,[3] or simply Samsung (Korean: 삼성; RR: samseong [samsʌŋ]) (stylized as SΛMSUNG), is a South Korean multinational manufacturing conglomerate headquartered in Samsung Town, Seoul, South Korea",
                            ImageUrl = "https://fdn2.gsmarena.com/vv/pics/samsung/samsung-galaxy-a23-1.jpg",
                            Name = "Samsung A 40",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 500,
                            UpdatedDate = new DateTime(2023, 7, 1, 11, 23, 47, 479, DateTimeKind.Local).AddTicks(1344)
                        });
                });

            modelBuilder.Entity("ShoppingCart_API_Application.Models.ProductNumber", b =>
                {
                    b.Property<int>("ProductNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductNo"));

                    b.Property<DateTime>("CreatedData")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductNo");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductNumbers");
                });

            modelBuilder.Entity("ShoppingCart_API_Application.Models.ProductNumber", b =>
                {
                    b.HasOne("ShoppingCart_API_Application.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}