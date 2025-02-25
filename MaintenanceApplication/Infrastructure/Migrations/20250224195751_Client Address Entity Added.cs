using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientAddressEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apartment",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "OfferedServices");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientAddressId",
                table: "OfferedServices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferedServices_ClientAddressId",
                table: "OfferedServices",
                column: "ClientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddresses_ClientId",
                table: "ClientAddresses",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedServices_ClientAddresses_ClientAddressId",
                table: "OfferedServices",
                column: "ClientAddressId",
                principalTable: "ClientAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferedServices_ClientAddresses_ClientAddressId",
                table: "OfferedServices");

            migrationBuilder.DropTable(
                name: "ClientAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OfferedServices_ClientAddressId",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "ClientAddressId",
                table: "OfferedServices");

            migrationBuilder.AddColumn<string>(
                name: "Apartment",
                table: "OfferedServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "OfferedServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "OfferedServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "OfferedServices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "OfferedServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
