using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Company_Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Krs = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Company_Clients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualClients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualClients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_IndividualClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Email", "Phone" },
                values: new object[,]
                {
                    { 1, "ul. Kowalska 1, Warszawa", "jan.kowalski@gmail.com", "500600700" },
                    { 2, "ul. Nowaka 5, Kraków", "anna.nowak@gmail.com", "501601701" },
                    { 3, "ul. Wiśniewska 3, Gdańsk", "piotr.wisniewski@gmail.com", "502602702" },
                    { 4, "ul. Wójcika 7, Poznań", "maria.wojcik@gmail.com", "503603703" },
                    { 5, "ul. Kamińska 9, Wrocław", "tomasz.kaminski@gmail.com", "504604704" },
                    { 6, "ul. Firmowa 1, Warszawa", "kontakt@abcsp.pl", "600700800" },
                    { 7, "ul. Biznesowa 2, Kraków", "biuro@xyzsa.pl", "601701801" },
                    { 8, "ul. Handlowa 3, Gdańsk", "office@techsolutions.pl", "602702802" },
                    { 9, "ul. Przemysłowa 4, Poznań", "info@megacorp.pl", "603703803" },
                    { 10, "ul. Korporacyjna 5, Wrocław", "kontakt@globaltech.pl", "604704804" }
                });

            migrationBuilder.InsertData(
                table: "Company_Clients",
                columns: new[] { "ClientId", "CompanyName", "Krs" },
                values: new object[,]
                {
                    { 6, "ABC Sp. z o.o.", "0000123456" },
                    { 7, "XYZ S.A.", "0000234567" },
                    { 8, "Tech Solutions Sp. z o.o.", "0000345678" },
                    { 9, "MegaCorp S.A.", "0000456789" },
                    { 10, "GlobalTech Sp. z o.o.", "0000567890" }
                });

            migrationBuilder.InsertData(
                table: "IndividualClients",
                columns: new[] { "ClientId", "FirstName", "IsDeleted", "LastName", "Pesel" },
                values: new object[,]
                {
                    { 1, "Jan", false, "Kowalski", "90010112345" },
                    { 2, "Anna", false, "Nowak", "85020223456" },
                    { 3, "Piotr", false, "Wiśniewski", "78030334567" },
                    { 4, "Maria", false, "Wójcik", "92040445678" },
                    { 5, "Tomasz", false, "Kamiński", "88050556789" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_Clients");

            migrationBuilder.DropTable(
                name: "IndividualClients");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
