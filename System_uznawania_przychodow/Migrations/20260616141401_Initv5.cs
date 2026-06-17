using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Initv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 10);

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "Software_Discounts ",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Discounts ", x => new { x.SoftwareId, x.DiscountId });
                    table.ForeignKey(
                        name: "FK_Software_Discounts _Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_Discounts _Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountId", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Black Friday", 15.00m },
                    { 2, "Cyber Monday", 20.00m },
                    { 3, "Wiosenna Promocja", 10.00m },
                    { 4, "Niezwykłe rabaty", 17.50m }
                });

            migrationBuilder.InsertData(
                table: "Software_Discounts ",
                columns: new[] { "DiscountId", "SoftwareId", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2026, 11, 30), new DateOnly(2026, 3, 23) },
                    { 3, 1, new DateOnly(2026, 5, 31), new DateOnly(2026, 3, 1) },
                    { 4, 1, new DateOnly(2026, 8, 31), new DateOnly(2026, 6, 1) },
                    { 2, 2, new DateOnly(2026, 12, 2), new DateOnly(2026, 6, 30) },
                    { 3, 2, new DateOnly(2026, 5, 31), new DateOnly(2026, 3, 1) },
                    { 4, 3, new DateOnly(2026, 9, 30), new DateOnly(2026, 4, 1) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_Discounts _DiscountId",
                table: "Software_Discounts ",
                column: "DiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Software_Discounts ");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "SoftwareId", "Category", "CurrentVersion", "Description", "Name" },
                values: new object[,]
                {
                    { 7, "Zarządzanie projektami", "3.5.2", "Narzędzie do zarządzania projektami i zadaniami zespołów", "ProjectTrack" },
                    { 8, "Medycyna", "1.2.0", "System do zarządzania dokumentacją medyczną pacjentów", "MedRecord" },
                    { 9, "Prawo", "2.1.4", "Oprogramowanie do zarządzania dokumentacją prawną kancelarii", "LegalDocs" },
                    { 10, "Handel", "4.4.1", "System kasowy i sprzedażowy dla sklepów detalicznych", "RetailPOS" }
                });
        }
    }
}
