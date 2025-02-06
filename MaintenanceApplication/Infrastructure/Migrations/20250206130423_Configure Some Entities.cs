using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOtps");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c292b81-11ec-4161-bf09-232b7bf06cce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e365faa4-40b4-4334-ac92-a403f3c87414");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c47a55d9-6b16-4019-a237-a881ee5f1c45", "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c47a55d9-6b16-4019-a237-a881ee5f1c45");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f845e3d-43d2-4373-811b-ceab15b3b290", null, "Freelancer", "FREELANCER" },
                    { "5c2d980c-b414-4948-9e4b-beec614a8c32", null, "Client", "CLIENT" },
                    { "eee74791-4202-4e3d-a56c-64a37491a364", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f6665896-ae75-45d2-a872-cbf78f64f6a4", 0, "d922ece7-d43e-4f6a-a5bb-ebcb429974ae", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEI8juRnCgOlan/gf2cYfqkWEnp4+WCb4W4TFP+/nVvSTShXkzx+yQ+hyiEWKP238zg==", null, false, "fc02773b-13ca-43b3-8958-ef1e147bd001", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eee74791-4202-4e3d-a56c-64a37491a364", "f6665896-ae75-45d2-a872-cbf78f64f6a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f845e3d-43d2-4373-811b-ceab15b3b290");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c2d980c-b414-4948-9e4b-beec614a8c32");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eee74791-4202-4e3d-a56c-64a37491a364", "f6665896-ae75-45d2-a872-cbf78f64f6a4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eee74791-4202-4e3d-a56c-64a37491a364");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6665896-ae75-45d2-a872-cbf78f64f6a4");

            migrationBuilder.CreateTable(
                name: "UserOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Otp = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c292b81-11ec-4161-bf09-232b7bf06cce", null, "Client", "CLIENT" },
                    { "c47a55d9-6b16-4019-a237-a881ee5f1c45", null, "Admin", "ADMIN" },
                    { "e365faa4-40b4-4334-ac92-a403f3c87414", null, "Freelancer", "FREELANCER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c", 0, "57aa7056-cb10-41b3-bba8-e340ab3ac124", "ApplicationUser", "admin@gmail.com", true, "System Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEC3Xp9XT718/bSQWd8F+XPVW84e42e2yrpbuECME7bdCiFGl8xyqQvSxTpQwHzM39Q==", null, false, "4069cae5-c28e-4bc9-a7fc-9fa604ea10c8", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c47a55d9-6b16-4019-a237-a881ee5f1c45", "04e3259e-e1ac-4086-a66e-c5e49fbdfd0c" });
        }
    }
}
