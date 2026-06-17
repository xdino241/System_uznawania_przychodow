using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System_uznawania_przychodow.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualClients_Clients_ClientId",
                table: "IndividualClients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualClients",
                table: "IndividualClients");

            migrationBuilder.RenameTable(
                name: "IndividualClients",
                newName: "Individual_Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Individual_Clients",
                table: "Individual_Clients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_Clients_Clients_ClientId",
                table: "Individual_Clients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individual_Clients_Clients_ClientId",
                table: "Individual_Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Individual_Clients",
                table: "Individual_Clients");

            migrationBuilder.RenameTable(
                name: "Individual_Clients",
                newName: "IndividualClients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualClients",
                table: "IndividualClients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualClients_Clients_ClientId",
                table: "IndividualClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
