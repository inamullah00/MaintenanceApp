using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAreaOfExpertise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerTopServices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a1cb165-3ef7-4b26-80a6-3935799a7f75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7523748-3324-44bd-9565-12108e39a124");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "be058a74-0327-48f6-adf3-a9009ff6c441", "1791d2c4-968d-4fd4-903a-ec917eb5560f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be058a74-0327-48f6-adf3-a9009ff6c441");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1791d2c4-968d-4fd4-903a-ec917eb5560f");

            migrationBuilder.DropColumn(
                name: "AreaOfExpertise",
                table: "Freelancers");

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceLevel",
                table: "Freelancers",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FreelancerService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerService_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", null, "Admin", "ADMIN" },
                    { "30964f5f-db56-407e-9965-8f3244ce4b66", null, "Freelancer", "FREELANCER" },
                    { "c50cda6c-fa8a-4041-9760-c753061a82c1", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "98d6d110-3b15-48fe-9029-096340d116b2", 0, "f0f608f0-eb14-48dd-bc06-781373a81cf8", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOz6TSfELhIWM3Ahrhl8kuJ6H2CXzPMac3teaY6JD5TDF781M62qBHsM9tAnzAjD9w==", null, false, "1bd495fc-7234-4fb8-8fcd-cb1798647494", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", "98d6d110-3b15-48fe-9029-096340d116b2" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerService_FreelancerId",
                table: "FreelancerService",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerService_ServiceId",
                table: "FreelancerService",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerService");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30964f5f-db56-407e-9965-8f3244ce4b66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c50cda6c-fa8a-4041-9760-c753061a82c1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "14cdd25a-c79f-465d-9246-d0b50c9b2ce8", "98d6d110-3b15-48fe-9029-096340d116b2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14cdd25a-c79f-465d-9246-d0b50c9b2ce8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98d6d110-3b15-48fe-9029-096340d116b2");

            migrationBuilder.AlterColumn<string>(
                name: "ExperienceLevel",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaOfExpertise",
                table: "Freelancers",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FreelancerTopServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FreelancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerTopServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerTopServices_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerTopServices_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a1cb165-3ef7-4b26-80a6-3935799a7f75", null, "Freelancer", "FREELANCER" },
                    { "be058a74-0327-48f6-adf3-a9009ff6c441", null, "Admin", "ADMIN" },
                    { "f7523748-3324-44bd-9565-12108e39a124", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1791d2c4-968d-4fd4-903a-ec917eb5560f", 0, "7940b0e6-e14e-400f-a6e6-f1f71cf8e30e", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBmopGyRYEadU5C3xjiBdG5/t2PtdCGUaGtSOjtjhvNoMlk5yITQgtdHFv/bE1EP9g==", null, false, "72a9ef4b-39fa-4ebf-ad38-7fd4ad25db1e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "be058a74-0327-48f6-adf3-a9009ff6c441", "1791d2c4-968d-4fd4-903a-ec917eb5560f" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerTopServices_FreelancerId",
                table: "FreelancerTopServices",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerTopServices_ServiceId",
                table: "FreelancerTopServices",
                column: "ServiceId");
        }
    }
}
