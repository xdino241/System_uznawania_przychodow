using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Initv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Software_Discounts _Discounts_DiscountId",
                table: "Software_Discounts ");

            migrationBuilder.DropForeignKey(
                name: "FK_Software_Discounts _Softwares_SoftwareId",
                table: "Software_Discounts ");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Software_Discounts ",
                table: "Software_Discounts ");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Software_Discounts ");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Software_Discounts ");

            migrationBuilder.RenameTable(
                name: "Software_Discounts ",
                newName: "Software_Discounts");

            migrationBuilder.RenameIndex(
                name: "IX_Software_Discounts _DiscountId",
                table: "Software_Discounts",
                newName: "IX_Software_Discounts_DiscountId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Discounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Discounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Software_Discounts",
                table: "Software_Discounts",
                columns: new[] { "SoftwareId", "DiscountId" });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 11, 30), new DateOnly(2026, 11, 23) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 12, 2), new DateOnly(2026, 11, 30) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 5, 31), new DateOnly(2026, 3, 1) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "DiscountId",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 8, 31), new DateOnly(2026, 6, 1) });

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Discounts_Discounts_DiscountId",
                table: "Software_Discounts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Discounts_Softwares_SoftwareId",
                table: "Software_Discounts",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "SoftwareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Software_Discounts_Discounts_DiscountId",
                table: "Software_Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Software_Discounts_Softwares_SoftwareId",
                table: "Software_Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Software_Discounts",
                table: "Software_Discounts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "Software_Discounts",
                newName: "Software_Discounts ");

            migrationBuilder.RenameIndex(
                name: "IX_Software_Discounts_DiscountId",
                table: "Software_Discounts ",
                newName: "IX_Software_Discounts _DiscountId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Software_Discounts ",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Software_Discounts ",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Software_Discounts ",
                table: "Software_Discounts ",
                columns: new[] { "SoftwareId", "DiscountId" });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 11, 30), new DateOnly(2026, 3, 23) });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 3, 1 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 5, 31), new DateOnly(2026, 3, 1) });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 4, 1 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 8, 31), new DateOnly(2026, 6, 1) });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 12, 2), new DateOnly(2026, 6, 30) });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 3, 2 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 5, 31), new DateOnly(2026, 3, 1) });

            migrationBuilder.UpdateData(
                table: "Software_Discounts ",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 4, 3 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 9, 30), new DateOnly(2026, 4, 1) });

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Discounts _Discounts_DiscountId",
                table: "Software_Discounts ",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Discounts _Softwares_SoftwareId",
                table: "Software_Discounts ",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "SoftwareId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
