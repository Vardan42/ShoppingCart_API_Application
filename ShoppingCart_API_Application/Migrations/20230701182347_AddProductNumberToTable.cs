using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart_API_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNumberToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductNumbers",
                columns: table => new
                {
                    ProductNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNumbers", x => x.ProductNo);
                    table.ForeignKey(
                        name: "FK_ProductNumbers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 7, 1, 11, 23, 47, 479, DateTimeKind.Local).AddTicks(1290), new DateTime(2023, 7, 1, 11, 23, 47, 479, DateTimeKind.Local).AddTicks(1344) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductNumbers_ProductId",
                table: "ProductNumbers",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductNumbers");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 6, 30, 10, 41, 20, 786, DateTimeKind.Local).AddTicks(7967), new DateTime(2023, 6, 30, 10, 41, 20, 786, DateTimeKind.Local).AddTicks(8024) });
        }
    }
}
