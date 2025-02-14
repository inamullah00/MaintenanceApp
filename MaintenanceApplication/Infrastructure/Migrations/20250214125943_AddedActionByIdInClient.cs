using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedActionByIdInClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionById",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ActionById",
                table: "Clients",
                column: "ActionById");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_ActionById",
                table: "Clients",
                column: "ActionById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_ActionById",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ActionById",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ActionById",
                table: "Clients");
        }
    }
}
