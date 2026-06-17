using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Initv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Softwares",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupportYears = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractPayments",
                columns: table => new
                {
                    ContractPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayments", x => x.ContractPaymentId);
                    table.ForeignKey(
                        name: "FK_ContractPayments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "System do zarządzania finansami", 5000.00m });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Platforma e-learningowa", 3500.00m });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "System do zarządzania HR", 7000.00m });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 4,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Magazyn i logistyka", 8500.00m });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 5,
                columns: new[] { "Description", "Price" },
                values: new object[] { "System CRM", 4200.00m });

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 6,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Księgowość", 2000.00m });

            migrationBuilder.CreateIndex(
                name: "IX_ContractPayments_ContractId",
                table: "ContractPayments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SoftwareId",
                table: "Contracts",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractPayments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Softwares");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 1,
                column: "Description",
                value: "System do zarządzania finansami firmowymi i raportowania księgowego");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 2,
                column: "Description",
                value: "Platforma e-learningowa do tworzenia kursów online i zarządzania uczniami");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 3,
                column: "Description",
                value: "System do zarządzania zasobami ludzkimi, urlopami i wynagrodzeniami");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 4,
                column: "Description",
                value: "Oprogramowanie do zarządzania magazynem i logistyką");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 5,
                column: "Description",
                value: "System CRM do zarządzania relacjami z klientami i sprzedażą");

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "SoftwareId",
                keyValue: 6,
                column: "Description",
                value: "Prosty system księgowy dla małych firm");
        }
    }
}
