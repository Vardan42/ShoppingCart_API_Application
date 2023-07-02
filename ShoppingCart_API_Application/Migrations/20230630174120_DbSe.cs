using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart_API_Application.Migrations
{
    /// <inheritdoc />
    public partial class DbSe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { 1, "", new DateTime(2023, 6, 30, 10, 41, 20, 786, DateTimeKind.Local).AddTicks(7967), "Samsung Group,[3] or simply Samsung (Korean: 삼성; RR: samseong [samsʌŋ]) (stylized as SΛMSUNG), is a South Korean multinational manufacturing conglomerate headquartered in Samsung Town, Seoul, South Korea", "https://fdn2.gsmarena.com/vv/pics/samsung/samsung-galaxy-a23-1.jpg", "Samsung A 40", 5, 200.0, 500, new DateTime(2023, 6, 30, 10, 41, 20, 786, DateTimeKind.Local).AddTicks(8024) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
