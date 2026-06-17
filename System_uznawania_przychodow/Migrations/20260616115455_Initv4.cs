using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Initv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CurrentVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.SoftwareId);
                });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "SoftwareId", "Category", "CurrentVersion", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Finanse", "3.2.1", "System do zarządzania finansami firmowymi i raportowania księgowego", "FinanSoft" },
                    { 2, "Edukacja", "2.0.5", "Platforma e-learningowa do tworzenia kursów online i zarządzania uczniami", "EduPlatform" },
                    { 3, "HR", "4.1.0", "System do zarządzania zasobami ludzkimi, urlopami i wynagrodzeniami", "HRManager" },
                    { 4, "Logistyka", "1.8.3", "Oprogramowanie do zarządzania magazynem i logistyką", "WarehousePro" },
                    { 5, "Sprzedaż", "5.0.0", "System CRM do zarządzania relacjami z klientami i sprzedażą", "CRM Master" },
                    { 6, "Finanse", "2.3.7", "Prosty system księgowy dla małych firm", "AccountKeeper" },
                    { 7, "Zarządzanie projektami", "3.5.2", "Narzędzie do zarządzania projektami i zadaniami zespołów", "ProjectTrack" },
                    { 8, "Medycyna", "1.2.0", "System do zarządzania dokumentacją medyczną pacjentów", "MedRecord" },
                    { 9, "Prawo", "2.1.4", "Oprogramowanie do zarządzania dokumentacją prawną kancelarii", "LegalDocs" },
                    { 10, "Handel", "4.4.1", "System kasowy i sprzedażowy dla sklepów detalicznych", "RetailPOS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Softwares");
        }
    }
}
